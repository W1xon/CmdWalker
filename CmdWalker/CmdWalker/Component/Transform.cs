namespace CmdWalker;

internal class Transform(Vector position, Vector size)
{
    public Vector Position { get; set; } = position;
    public Vector Size { get; set; } = size;
}