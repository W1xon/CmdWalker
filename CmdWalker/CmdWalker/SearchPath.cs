namespace CmdWalker
{
    internal class SearchPath
    {
        private TileMap _tile;
        private Vector _size;
        private readonly Dictionary<Vector, Vector> _cameFrom = new();
        private readonly Dictionary<Vector, int> _gScores = new();
        private readonly PriorityQueue<Vector, int> _frontier = new();

        private readonly List<Vector> _path = new();

        public SearchPath(TileMap tile, Vector size)
        {
            _tile = tile;
            _size = size;
        }
        private List<Vector> GetPath(Vector start, Vector goal)
        {
            _cameFrom.Clear();
            _gScores.Clear();
            _frontier.Clear();
            _path.Clear();
    
            _frontier.Enqueue(start, 0);
            _cameFrom[start] = default;
            _gScores[start] = 0;

            while (_frontier.Count > 0)
            {
                var current = _frontier.Dequeue();
                if (ContainsGoal(current, goal))
                {
                    goal = current;
                    break;
                }

                foreach (var neighbor in GetNeighbours(current))
                {
                    if (IsBlocked(_tile.Tiles, neighbor)) continue;
                    var newGScore = _gScores[current] + GetCost();

                    if (!_gScores.TryGetValue(neighbor, out int existingGScore) || newGScore < existingGScore)
                    {
                        _gScores[neighbor] = newGScore;
                        int priority = newGScore + Heuristic(neighbor, goal);
                        _frontier.Enqueue(neighbor, priority);
                        _cameFrom[neighbor] = current;
                    }
                }
            }

            var pos = goal;
            while (pos != start)
            {
                _path.Add(pos);
                if (!_cameFrom.TryGetValue(pos, out var prev)) return null;
                pos = prev;
            }
            _path.Reverse();
            return new List<Vector>(_path);
        }

        public Vector GetNextPosition(Vector start, Vector goal)
        {
            var path = GetPath(start, goal);
            if (path == null || path.Count == 0) return Vector.zero;
            return path.First();
        }
        private List<Vector> GetNeighbours(Vector position)
        {
            var directions = new Vector[] { Vector.up, Vector.down, Vector.left, Vector.right };
            var neighbors = new List<Vector>();
            foreach (var direction in directions)
            {
                Vector target = position + direction;
                if (target.Y < 0 || target.X < 0 || target.X > _tile.Size.X || target.Y > _tile.Size.Y) continue;
                if (_tile.GetCell(target) == RenderPalette.GetChar(TileType.Wall)) continue;
                neighbors.Add(target);
            }
            return neighbors;
        }

        private int GetCost()
        {
            // Пока что стоимость перехода фиксированная
            return 1;
        }

        private int Heuristic(Vector a, Vector b) => Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);


        public bool IsBlocked(char[,] field, Vector pos)
        {
            for (int x = 0; x < _size.X; x++)
            {
                for (int y = 0; y < _size.Y; y++)
                {
                    int newX = pos.X + x;
                    int newY = pos.Y + y;

                    if (newX < 0 || newX >= field.GetLength(1) ||
                        newY < 0 || newY >= field.Length ||
                        field[newY, newX] == RenderPalette.GetChar(TileType.Wall))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool ContainsGoal(Vector pos, Vector goal)
        {
            for (int x = 0; x < _size.X; x++)
            {
                for (int y = 0; y < _size.Y; y++)
                {
                    int newX = pos.X + x;
                    int newY = pos.Y + y;
                    var target = new Vector(newX, newY);
                    if (target == goal)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
