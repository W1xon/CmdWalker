namespace CmdWalker;

internal class NGon : Polygon
{
    // Т.к. консоль, то по вертикали приходится уменьшать
    private float yScale = 1.8f; 
    
    public NGon(Vector center, int radius, int sides) : base(center)
    {
        _vertices = new List<Vector>(sides);
        for (int i = 0; i < sides; i++)
        {
            double angle = 2 * Math.PI / sides * i;
            int y = (int)(Math.Round(center.Y + radius * Math.Sin(angle)) / yScale);
            int x = (int)Math.Round(center.X + radius * Math.Cos(angle));

            _vertices.Add(new Vector(x, y));
        }
    }
}