namespace CmdWalker;

internal class Inventory
{
    public ICollectable ActiveItem { get; private set; }
    private List<Stack> _stacks = new List<Stack>();

    public void Equip(int index)
    {
        if (index >= _stacks.Count()) return;

        ActiveItem = _stacks[index].Item;
        ConsoleDebugView.InventoryInfo = Summary();
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

        ConsoleDebugView.InventoryInfo = Summary();
    }

    public void DropAll(ICollectable collectable)
    {
        _stacks.Remove(GetStack(collectable));
        ConsoleDebugView.InventoryInfo = Summary();
    }
    public void Drop(ICollectable collectable)
    {
        if (Contains(collectable))
        {
            if (collectable.IsStackable())
                GetStack(collectable).Count--;
            if (!collectable.IsStackable() || GetStack(collectable).Count <= 0)
                _stacks.Remove(GetStack(collectable));
        }
        ConsoleDebugView.InventoryInfo = Summary();
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
        }
    }

    public bool Contains(ICollectable item) => Contains(item.GetName());
    public bool Contains(string name) => _stacks.FirstOrDefault(i => i.Item.GetName() == name) != null;
    public bool Contains(int id) => _stacks.FirstOrDefault(i => i.Item.GetId() == id) != null;

    private (char[][],  ConsoleColor[]) Summary()
    {
        char[][] inventory = new char[_stacks.Count * 3][];
        ConsoleColor[] consoleColors = new ConsoleColor[_stacks.Count];
        int k = 0;
        for (int i = 0; i < _stacks.Count; i++)
        {
            consoleColors[i] = _stacks[i].Item == ActiveItem ? ConsoleColor.Red : ConsoleColor.White;
            inventory[k++] =  "╔══════╗".ToCharArray();
            inventory[k++] = $"║{_stacks[i].Item.GetGlyph().Symbol,-2} x{_stacks[i].Count,2}║".ToCharArray();
            inventory[k++] =  "╚══════╝".ToCharArray();
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