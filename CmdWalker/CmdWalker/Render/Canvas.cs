namespace CmdWalker;

internal class Canvas : RenderObject
{
    private List<RenderObject> _child = new List<RenderObject>();
    public override void Draw(Vector position, string symbol, RenderObject renderObject, ConsoleColor color = ConsoleColor.White)
    {
        Vector correctPos = position + Position;
        Console.SetCursorPosition(correctPos.X, correctPos.Y);
        Console.ForegroundColor = color;
        Console.Write(symbol);
    }

    public override void AddChild(RenderObject renderObject, Vector position)
    {
        renderObject.Position = position;
        renderObject.AddParent(this);
        if(!_child.Contains(renderObject))
            _child.Add(renderObject);
    }
    
}