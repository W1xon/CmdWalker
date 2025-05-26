namespace CmdWalker
{
    internal class Map : RenderObject
    {
        public TileMap Plane { get; set; } = new TileMap();
        public TileMap Carcas { get; set; } = new TileMap();
        public List<GameEntity> Entities { get => new List<GameEntity>(_entites); }
        public Vector Size { get; set; }
        private List<GameEntity> _entites = new List<GameEntity>();
        private MapVisualizer _effectRendering;
        public void InitializePlane()
        {
            Size = Carcas.Size;
            Carcas.CopyTo(Plane);
            _effectRendering = new MapVisualizer(this);
        }
        public void Show()
        {
            _effectRendering.RenderEntireMap();
        }

        public void Show(Vector pos)
        {
            _effectRendering.AnimateReachableArea(pos);
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

        public void SetCells(Vector position, string symbols)
        {
            SetCell(position, symbols);
        }
        public void SetCells(Vector position, IVisual visual, bool isStandartColor = false)
        {
            ConsoleColor color = isStandartColor ? ConsoleColor.White : visual.Color;
            for (int y = 0; y < visual.Representation.GetLength(0); y++)
            {
                Vector newPos = position + new Vector(0, y);
                SetCell(newPos, visual.Representation.GetRowAsString(y).ToString(), color);
            }
        }
        public char GetCell(Vector pos, bool isCarcas = false)
        {
            return isCarcas ? Carcas.GetCell(pos) : Plane.GetCell(pos);
        }
        private void SetCell(Vector pos, string symbols, ConsoleColor color = ConsoleColor.White)
        {
            Draw(pos, symbols,  this, color);
            foreach (var symbol in symbols)
            {
                Plane.SetCell(pos, symbol);
            }
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
