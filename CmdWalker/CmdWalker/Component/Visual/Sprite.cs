namespace CmdWalker;

internal class Sprite : IVisual
{

    public char[,] Representation
    {
        get => _sprite;
        set => _sprite = value;
    }

    public ConsoleColor Color => _color;

    public Vector Size => new Vector(
        _sprite.GetLength(1),
        _sprite.GetLength(0)
    );

    public string LeftAdditive { get; set; }
    public string RightAdditive { get; set; }
    public string UpAdditive { get; set; }
    public string DownAdditive { get; set; }

    private char[,] _sprite;
    private readonly ConsoleColor _color;

    public Sprite(char[,] sprite, ConsoleColor color)
    {
        
        _sprite = sprite;
        _color = color;
    }
}