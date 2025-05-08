namespace CmdWalker;

internal abstract class RenderObject
{
    public Vector Position { get; set; }
    protected RenderObject _parent; 
    public abstract void Draw(Vector position, string symbol, RenderObject renderObject, ConsoleColor color = ConsoleColor.White);
    
    public void AddParent(RenderObject parent)
    {
        _parent = parent;
    }
    public virtual void AddChild(RenderObject renderObject, Vector position)
    {
        throw new NotImplementedException();
    }
}
