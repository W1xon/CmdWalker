namespace CmdWalker;

internal abstract class CarcassGenerator
{
    protected MapTemplate _template;
    protected char[,] _field;
    protected int _width;
    protected int _height;
    protected Random _rand = new Random();

    public CarcassGenerator(MapTemplate template)
    {
        _template = template;
        _width = _template.Size.X;
        _height = _template.Size.Y;
        
        _field = new char[_height, _width];
        _field.Fill(' ');
    }
    public abstract char[,] Generate();
    protected void AddShapeContour(Polygon polygon, char glyph)
    {
        var vertices = polygon.GetVertices();
        if (vertices.Count < 2) return;

        int x0, x1, y0, y1;
        for (var i = 0; i < vertices.Count; i++)
        {
            int next = (i + 1) % vertices.Count;
            x0 = vertices[i].X;
            x1 = vertices[next].X;
            y0 = vertices[i].Y;
            y1 = vertices[next].Y;

            var steep = Math.Abs(y1 - y0) > Math.Abs(x1 - x0);
            if (steep)
            {
                Swap(ref x0, ref y0);
                Swap(ref x1, ref y1);
            }

            if (x0 > x1)
            {
                Swap(ref x0, ref x1);
                Swap(ref y0, ref y1);
            }

            int deltaX = x1 - x0;
            int deltaY = Math.Abs(y1 - y0);
            int error = deltaX / 2;
            int yStep = (y0 < y1) ? 1 : -1;
            int y = y0;


            for (int x = x0; x <= x1; x++)
            {
                int col = steep ? y : x;
                int row = steep ? x : y;
                _field[row, col] = glyph;
                error -= deltaY;
                if (error < 0)
                {
                    y += yStep;
                    error += deltaX;
                }
            }
        }
    }
    private void Swap(ref int a, ref int b) =>  (a, b) = (b, a);
}