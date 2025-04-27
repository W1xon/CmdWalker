namespace CmdWalker
{
    internal class Map
    {
        public char[,] Plane { get; set; }
        public char[,] Carcas { get; set; }
        public List<GameEntity> Entities { get => new List<GameEntity>(_entites); }
        public Vector Size { get; set; }
        private List<GameEntity> _entites = new List<GameEntity>();
        public void InitializePlane()
        {
            Size = new Vector(Carcas.GetLength(1), Carcas.GetLength(0));
            Plane = Carcas.DeepCopy();
        }
        public void Show()
        {
            for (int y = 0; y < Size.Y; y++)
            {
                for (int x = 0; x < Size.X; x++)
                {
                    Console.Write(GetCell(new Vector(x, y)));
                }
                Console.WriteLine();
            }
        }
        public void SpawnEntity(GameEntity entity)
        {
            if (TryAddEntity(entity))
            {
                entity.BindToMap(this);
                SetCells(entity.Position, entity.Visual);
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
            Console.ForegroundColor = color;
            Console.SetCursorPosition(pos.X, pos.Y);
            Console.Write(symbol);
            Plane[pos.Y, pos.X] = symbol;
            Console.ForegroundColor = ConsoleColor.White;
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
            // Т.к. массив, то координаты инвертированны
            return isCarcas ? Carcas[pos.Y, pos.X] : Plane[pos.Y, pos.X];
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
    }
}
