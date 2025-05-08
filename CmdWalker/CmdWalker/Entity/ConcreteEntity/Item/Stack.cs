namespace CmdWalker;

internal class Stack(ICollectable item, int count)
{
    public ICollectable Item { get; set; } = item;
    public int Count { get; set; } = count;
}