namespace CmdWalker;

internal class Canvas : RenderObject
{
    private List<RenderObject> _child = [];
    private char[] _screenBuffer;
    private char[] _oldBuffer;
    private ConsoleColor[] _colorBuffer;
    private Vector _size;
    public Canvas(Vector size)
    {
        
        _screenBuffer = new char[size.X * size.Y];
        _oldBuffer = new char[size.X * size.Y];
        _colorBuffer = new ConsoleColor[size.X * size.Y];
        _size = size;
    }
    public override void Draw(Vector position, string symbol, RenderObject renderObject, ConsoleColor color = ConsoleColor.White)
    {
        Vector correctPos = position + Position;
        Console.SetCursorPosition(correctPos.X, correctPos.Y);
        Console.ForegroundColor = color;
        Console.Write(symbol);
    }
    public override void Write(Vector position, string symbol, RenderObject renderObject, ConsoleColor color = ConsoleColor.White)
    {

        for (int x = 0; x < symbol.Length; x++)
        {
            int index = (position.Y * _size.X) + position.X + x;
            if (index < 0 || index >= _screenBuffer.Length) continue;
    
            _screenBuffer[index] = symbol[x];
            _colorBuffer[index] = color;
        }
    }


    public void Render()
    {
        for (int i = 0; i < _screenBuffer.Length; i++)
        {
            if (_oldBuffer[i] == _screenBuffer[i])
                continue;

            int posY = i / _size.X;
            int posX = i % _size.X;

            Vector correctPos = new Vector(posX, posY) + Position;
            Console.SetCursorPosition(correctPos.X, correctPos.Y);
            Console.ForegroundColor = _colorBuffer[i];
            Console.Write(_screenBuffer[i]);

            _oldBuffer[i] = _screenBuffer[i];
        }
    }

    public override void AddChild(RenderObject renderObject, Vector position)
    {
        renderObject.Position = position;
        renderObject.AddParent(this);
        if(!_child.Contains(renderObject))
            _child.Add(renderObject);
    }
    
}
