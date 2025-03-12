namespace CmdWalker
{
    internal class Map
    {
        public char[][] Plane { get; set; }
        public char[][] Carcas { get; set; }
        public List<GameEntity> Entities { get => new List<GameEntity>(_entites); }
        public Vector Size { get; set; }
        private List<GameEntity> _entites = new List<GameEntity>();
        public void InitializePlane()
        {
            Size = new Vector(Carcas[0].Length, Carcas.Length);
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
                SetCells(entity.GetPositions(), entity.Glyph, entity.BodyColor);
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
        public void SetCell(Vector pos, char symbol, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.SetCursorPosition(pos.X, pos.Y);
            Console.Write(symbol);
            Plane[pos.Y][pos.X] = symbol;
            Console.ForegroundColor = ConsoleColor.White;
        }
        public void SetCells(Vector[] vectors, string symbols, ConsoleColor color = ConsoleColor.White)
        {
            for (int x = 0; x < vectors.Length; x++)
            {
                SetCell(vectors[x], symbols[x], color);
            }
        }
        public char GetCell(Vector pos, bool isCarcas = false)
        {
            // Т.к. массив, то координаты инвертированны
            if (isCarcas) return Carcas[pos.Y][pos.X];
            return Plane[pos.Y][pos.X];
            
        }
        private bool TryAddEntity(GameEntity entity)
        {
            if (_entites.Contains(entity) || entity == null) return false;
            _entites.Add(entity);
            return true;
        }
    }
}
