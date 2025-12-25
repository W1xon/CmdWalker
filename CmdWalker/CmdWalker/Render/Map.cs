namespace CmdWalker
{
    internal class Map : RenderObject
    {
        public TileMap Plane { get; set; } = new TileMap();
        public TileMap Carcas { get; set; } = new TileMap();
        public Vector Size { get; set; }
        public EntityManager EntityManager { get; private set; }
        public Map()
        {
            EntityManager = new EntityManager(this);
        }
        public void InitializePlane()
        {
            Size = Carcas.Size;
            Carcas.CopyTo(Plane);
        }
        public void BuildStructure(IStructure structure)
        {
            structure.Build(this);
        }
        public bool IsWithinBounds(Vector pos) => Carcas.IsWithinBounds(pos);        
        public override void Draw(Vector position, string symbol, RenderObject renderObject, ConsoleColor color = ConsoleColor.White)
        {
            _parent.Draw(position, symbol,  this, color);
        }
        public void SetCells(Vector[] positions, char[] symbols)
        {
            for(int i = 0; i < positions.Length; i++) 
            {
                RestoreCell(positions[i], symbols[i].ToString());
            }
        }
        public void SetCells(Vector position, IVisual visual, bool isStandartColor = false)
        {
            ConsoleColor color = isStandartColor ? ConsoleColor.White : visual.Color;
            for (int y = 0; y < visual.Representation.GetLength(0); y++)
            {
                Vector newPos = position + new Vector(0, y);
                DrawOverlayCell(newPos, visual.Representation.GetRowAsString(y), color);
            }
        }
        public char GetCell(Vector pos, bool isCarcas = false)
        {
            return isCarcas ? Carcas.GetCell(pos) : Plane.GetCell(pos);
        }
        
        private void DrawOverlayCell(Vector pos, string symbols, ConsoleColor color = ConsoleColor.White)
        {
            for (int i = 0; i < symbols.Length; i++)
            {
                char symbol = symbols[i];

                if (symbol == ' ')
                    continue;
                var posOffset = new Vector(pos.X + i, pos.Y);
                Draw(posOffset, symbol.ToString(), this, color);
                Plane.SetCell(posOffset, symbol);
            }
        }
        private void RestoreCell(Vector pos, string symbols)
        {
            for (int i = 0; i < symbols.Length; i++)
            {
                char symbol = symbols[i];
                Vector p = new(pos.X + i, pos.Y);

                Draw(p, symbol.ToString(), this, ConsoleColor.White);
                Plane.SetCell(p, symbol);
            }
        }

    }
}