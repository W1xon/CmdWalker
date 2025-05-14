namespace CmdWalker;

internal class Rectangle : Polygon
{
    public int X => _transform.Position.X;
    public int Y => _transform.Position.Y;
    public int Left => X;
    public int Right => X + _transform.Size.X;
    public int Top => Y;
    public int Bottom => Y + _transform.Size.Y;
    public int Height => _transform.Size.Y;
    public int Width => _transform.Size.X;
    private Transform _transform;
    public Rectangle(Vector position, Vector size) : base(
        new Vector(position.X, position.Y),
            new Vector(position.X + size.X, position.Y),
            new Vector(position.X + size.X, position.Y + size.Y),
            new Vector(position.X, position.Y + size.Y))
    {
        _transform = new Transform(position, size);
    }
}