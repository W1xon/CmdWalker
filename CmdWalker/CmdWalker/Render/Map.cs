namespace CmdWalker
{
    internal class Map : RenderObject
    {
        public TileMap Plane { get; set; } = new TileMap();
        public TileMap Carcas { get; set; } = new TileMap();
        public List<GameEntity> Entities { get => new List<GameEntity>(_entites); }
        public Vector Size { get; set; }
        private List<GameEntity> _entites = new List<GameEntity>();
        public void InitializePlane()
        {
            Size = Carcas.Size;
            Carcas.CopyTo(Plane);
        }
        public void Show()
        {
            for (int y = 0; y < Size.Y; y++)
            {
                for (int x = 0; x < Size.X; x++)
                {
                    Vector pos = new Vector(x, y);
                    Draw(pos, GetCell(pos).ToString(), this );
                }
                Console.WriteLine();
            }
        }
        public void SpawnEntity(GameEntity entity)
        {
            if (TryAddEntity(entity))
            {
                entity.BindToMap(this);
            }
        }
        public void BuildStructure(IStructure structure)
        {
            structure.Build(this);
        }
        public void DeleteEntity(GameEntity entity)
        {
            if (!_entites.Contains(entity)) return;
            _entites.Remove(entity);
        }

        private void SetCell(Vector pos, char symbol, ConsoleColor color = ConsoleColor.White)
        {
            Draw(pos, symbol.ToString(),  this, color);
            
            Plane.SetCell(pos, symbol);
        }
        public void SetCells(Vector position, string symbols)
        {
            SetCells(position, new Glyph(symbols, ConsoleColor.White));
        }
        public void SetCells(Vector position, IVisual visual, bool isStandartColor = false)
        {
            ConsoleColor color = isStandartColor ? ConsoleColor.White : visual.Color;
            for (int y = 0; y < visual.Representation.GetLength(0); y++)
            {
                for (int x = 0; x < visual.Representation.GetLength(1); x++)
                {
                    Vector newPos = position + new Vector(x, y);
                    SetCell(newPos, visual.Representation[y,x], color);
                }
            }
        }
        public char GetCell(Vector pos, bool isCarcas = false)
        {
            return isCarcas ? Carcas.GetCell(pos) : Plane.GetCell(pos);
        }
        private bool TryAddEntity(GameEntity entity)
        {
            if (_entites.Contains(entity) && entity == null) return false;
            _entites.Add(entity);
            return true;
        }
        public List<T> GetEntity<T>() where T : GameEntity
        {
            return _entites.OfType<T>().ToList();
        }
        public bool IsWithinBounds(Vector pos)
        {
            return pos.X >= 0 && pos.X < Size.X && pos.Y >= 0 && pos.Y < Size.Y;
        }
        
        public override void Draw(Vector position, string symbol, RenderObject renderObject, ConsoleColor color = ConsoleColor.White)
        {
            _parent.Draw(position, symbol,  this, color);
        }
    }
}
