namespace CmdWalker;

internal abstract class RenderObject
{
    public Vector Position { get; set; }
    public abstract void Draw(Vector position, string symbol, RenderObject renderObject, ConsoleColor color = ConsoleColor.White);
    public virtual void AddParent(RenderObject renderObject)
    {
        throw new NotImplementedException();
    }
    public virtual void AddChild(RenderObject renderObject, Vector position)
    {
        throw new NotImplementedException();
    }
}