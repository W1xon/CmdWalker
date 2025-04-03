namespace CmdWalker
{
    abstract class GameEntity
    {
        public Vector Position { get; set; }
        public string Glyph { get; }
        public  ConsoleColor BodyColor { get; }
        protected Map _map;
        public virtual void Update() { }
        public GameEntity(Vector position, string glyph, ConsoleColor color)
        {
            Position = position;
            Glyph = glyph;
            BodyColor = color;
        }
        public void BindToMap(Map map)
        {
            _map = map;
        }
        public Vector[] GetPositions()
        {
            Vector[] vectors = new Vector[Glyph.Length];
            for (int x = 0; x < Glyph.Length; x++)
            {
                vectors[x] = new Vector(Position.X + x, Position.Y);
            }
            return vectors;
        }
        public bool IsEntity(Vector pos)
        {
            foreach(var vector in GetPositions())
            {
                if (pos == vector) return true;
            }
            return false;
        }
    }
}
