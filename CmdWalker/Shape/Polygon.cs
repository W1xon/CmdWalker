namespace CmdWalker;

internal abstract class Polygon(params Vector[] vertices)
{
    protected List<Vector> _vertices = vertices.ToList();
    public List<Vector> GetVertices() => _vertices;
}