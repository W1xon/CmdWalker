using System.Text;

namespace CmdWalker;

internal class Inventory
{
    public ICollectable ActiveItem { get; private set; }
    private List<Stack> _stacks = new List<Stack>();

    public void Equip(int index)
    {
        if (index >= _stacks.Count()) return;

        ActiveItem = _stacks[index].Item;
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

    public void Drop(bool dropAll = false)
    {
        if (Contains(ActiveItem))
        {
            if (ActiveItem.IsStackable() && !dropAll)
                GetStack(ActiveItem).Count--;
            if ((ActiveItem.IsStackable() && dropAll) || !ActiveItem.IsStackable() || GetStack(ActiveItem).Count <= 0)
                _stacks.Remove(GetStack(ActiveItem));
        }
        else
            ActiveItem = null;
        ConsoleDebugView.InventoryInfo = Summary();
    }

    public void Use(ICollectable item)
    {
        if (Contains(item))
            item.Execute();
    }

    public bool Contains(ICollectable item) => Contains(item.GetName());
    public bool Contains(string name) => _stacks.FirstOrDefault(i => i.Item.GetName() == name) != null;

    public string Summary()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("");
        foreach (var stack in _stacks)
        {
            stringBuilder.Append($"{stack.Item.GetName()}: {stack.Count};");
        }
        return stringBuilder.ToString();
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