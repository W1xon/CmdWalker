namespace CmdWalker;

internal class Inventory
{
    public ICollectable ActiveItem { get; private set; }
    private List<Stack> _stacks = new List<Stack>();

    private  GameEntity _owner;

    public Inventory(GameEntity owner)
    {
        _owner = owner;
    }
    public void Equip(int index)
    {
        if (index >= _stacks.Count()) return;
        if (ActiveItem == _stacks[index].Item)
        {
            UnEquip();
            return;
        }
        ActiveItem = _stacks[index].Item;
        BindToEntityGlyph(Vector.right);
        Debug.InventoryInfo = Summary();
    }

    public bool TryEquip(int index, Func<ICollectable, bool> isCorrectly)
    {
        if (_stacks.Count <= 0 || index >= _stacks.Count) return false;
        if (isCorrectly.Invoke(_stacks[index].Item))
        {
            Equip(index);
            return true;
        }
        if (ActiveItem == _stacks[index].Item)
            UnEquip();
        return false;
    }
    public void UnEquip()
    {
        ActiveItem = null;
        BindToEntityGlyph();
        Debug.InventoryInfo = Summary();
    }

    public void PickUp(ICollectable item)
    {
        if (Contains(item))
        {
            if (item.IsStackable())
                GetStack(item).Count++;
        }
        else
            _stacks.Add(new Stack(item, 1));

        Debug.InventoryInfo = Summary();
    }

    public void DropAll(ICollectable collectable)
    {
        _stacks.Remove(GetStack(collectable));
        
        Debug.InventoryInfo = Summary();
    }

    public void DropAll()
    {
        _stacks.Clear();
        Debug.InventoryInfo = Summary();
    }
    public void Drop(ICollectable collectable)
    {
        if (Contains(collectable))
        {
            if (collectable.IsStackable())
                GetStack(collectable).Count--;
            if (!collectable.IsStackable() || GetStack(collectable).Count <= 0)
            {
                _stacks.Remove(GetStack(collectable));
                UnbindFromEntityGlyph();
                if(collectable == ActiveItem)
                    ActiveItem = null;
            }
        }
        Debug.InventoryInfo = Summary();
    }

    public void Use()
    {
        if (Contains(ActiveItem))
            ActiveItem.Execute();
    }

    public void Use(Vector direction)
    {
        if (ActiveItem is Weapon weapon)
        {
            weapon.Fire(direction, this);
            BindToEntityGlyph(direction);
        }
        else if (ActiveItem is Projectile projectile) 
        { 
            projectile.Launch(direction);
            BindToEntityGlyph(direction);
            Drop(projectile);
        }
    }

    public bool TryUse(Vector direction, Func<ICollectable, bool> isCorrectly)
    {
        if (ActiveItem is not null && isCorrectly(ActiveItem))
        {
            Use(direction);
            return true;
        }
        return false;
    }

    public bool Contains(ICollectable item) => Contains(item.GetName());
    public bool Contains(string name) => _stacks.FirstOrDefault(i => i.Item.GetName() == name) != null;
    public bool Contains(int id) => _stacks.FirstOrDefault(i => i.Item.GetId() == id) != null;

    private void BindToEntityGlyph(Vector dir = default)
    {
        var visual = ActiveItem?.GetVisual();
        var symbol = visual is Glyph glyph ? glyph.Symbol : string.Empty;
        Unit unit = (Unit)_owner;
        unit?.ClearPreviousPosition();
        
        if (dir == default && unit != null)
        {
            unit.Visual.RightAdditive = string.Empty;
            unit.Visual.LeftAdditive = string.Empty;
            return;
        }
        _owner.Visual.RightAdditive = dir == Vector.right ? symbol : string.Empty;
        _owner.Visual.LeftAdditive  = dir == Vector.left  ? symbol : string.Empty;
    }

    private void UnbindFromEntityGlyph()
    {
        Unit unit = (Unit)_owner;
        unit?.ClearPreviousPosition();
        
        unit.Visual.RightAdditive = string.Empty;
        unit.Visual.LeftAdditive = string.Empty;
           
    }

    private (char[,],  ConsoleColor[]) Summary()
    {
        char[,] inventory = new char[_stacks.Count * 3,8];
        ConsoleColor[] consoleColors = new ConsoleColor[_stacks.Count];
        int k = 0;
        for (int i = 0; i < _stacks.Count; i++)
        {
            consoleColors[i] = _stacks[i].Item == ActiveItem ? ConsoleColor.Red : ConsoleColor.White;
            inventory.InsertString(k++, 0, "╔══════╗");
            if(_stacks[i].Item.GetVisual() is Glyph glyph)
            {
                inventory.InsertString(k++, 0, $"║{glyph.Symbol, - 2} x{_stacks[i].Count,2}║");
            }
            else
            {
                inventory.InsertString(k++, 0, $"║     ║");
            }
            inventory.InsertString(k++, 0, "╚══════╝");
        }
        return (inventory, consoleColors);
    }

    private Stack? GetStack(ICollectable item)
    {
        foreach (var t in _stacks)
        {
            if (t.Item.GetName() == item.GetName())
                return t;
        }
        return null;
    }
}