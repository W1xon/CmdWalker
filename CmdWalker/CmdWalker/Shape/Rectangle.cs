namespace CmdWalker;

internal class Rectangle : Polygon
{
    public int X => _position.X;
    public int Y => _position.Y;
    public int Left => X;
    public int Right => X + _size.X;
    public int Top => Y;
    public int Bottom => Y + _size.Y;
    public int Height => _size.Y;
    public int Width => _size.X;
    private Vector _size;
    private Vector _position;
    public Rectangle(Vector position, Vector size) : base(
        new Vector(position.X, position.Y),
            new Vector(position.X + size.X, position.Y),
            new Vector(position.X + size.X, position.Y + size.Y),
            new Vector(position.X, position.Y + size.Y))
    {
        _size = size;
        _position = position;
    }
}