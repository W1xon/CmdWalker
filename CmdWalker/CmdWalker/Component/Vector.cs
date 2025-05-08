namespace CmdWalker
{
    internal struct Vector
    {
        public int X { get; set; }
        public int Y { get; set; }
        public static readonly Vector left = new Vector(-1, 0); 
        public static readonly Vector right = new Vector(1, 0);
        public static readonly Vector up = new Vector(0, 1);
        public static readonly Vector down = new Vector(0, -1);
        public static readonly Vector zero = new Vector(0, 0);
        public static readonly Vector one = new Vector(1, 1);

        private static Random _rand = new Random();
        public Vector(int x, int y)
        {
            X = x;
            Y = y;
        }
        public static Vector operator +(Vector a, Vector b) => new Vector(a.X + b.X, a.Y + b.Y);
        public static Vector operator -(Vector a, Vector b) => new Vector(a.X - b.X, a.Y - b.Y);
        public static bool operator ==(Vector a, Vector b) =>  a.X == b.X && a.Y == b.Y;
        public static bool operator !=(Vector a, Vector b) =>  !(a.X == b.X && a.Y == b.Y);
        public static Vector operator *(Vector a, int n) => new Vector(a.X * n, a.Y * n);
        

        public static Vector GetRandom(Vector exclude = default, bool excludeZero = false)
        {
            var possibleVectors = new[]
            {
               new Vector(-1, -1), new Vector(-1, 0), new Vector(-1, 1),
               new Vector(0, -1),  new Vector(0, 0),  new Vector(0, 1),
               new Vector(1, -1),  new Vector(1, 0),  new Vector(1, 1)
            };
            var filtered = possibleVectors.Where(v => (v != exclude && (!excludeZero || v != zero))).ToArray();
            return filtered[_rand.Next(0, filtered.Length)];
        }
        
        public static int Distance(Vector a, Vector b)
        {
            return (int)Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
        }
        public Vector Abs() => new Vector(Math.Abs(X), Math.Abs(Y));

    }
}
