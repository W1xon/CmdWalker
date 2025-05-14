namespace CmdWalker
{
    internal class SearchPath
    {
        private Vector _size;
        private readonly Dictionary<Vector, Vector> _cameFrom = new();
        private readonly Dictionary<Vector, int> _gScores = new();
        private readonly PriorityQueue<Vector, int> _frontier = new();

        private readonly List<Vector> _path = new();

        public SearchPath(Vector size)
        {
            _size = size;
        }

        public Vector GetNextPosition(TileMap tile, Vector start, Vector goal, int maxIteration)
        {
            var path = GetPath(tile, start, goal, maxIteration );
            if (path == null || path.Count == 0) return Vector.zero;
            return path.First();
        }
        private List<Vector> GetPath(TileMap tile, Vector start, Vector goal, int maxIteration)
        {
            _cameFrom.Clear();
            _gScores.Clear();
            _frontier.Clear();
            _path.Clear();
    
            _frontier.Enqueue(start, 0);
            _cameFrom[start] = default;
            _gScores[start] = 0;
            int iteration = 0;
            while (_frontier.Count > 0)
            {
                var current = _frontier.Dequeue();
                if (ContainsGoal(current, goal))
                {
                    goal = current;
                    break;
                }

                foreach (var neighbor in GetNeighbours(tile, current))
                {
                    if (IsBlocked(tile.Tiles, neighbor)) continue;
                    var newGScore = _gScores[current] + GetCost();

                    if (newGScore < _gScores.GetValueOrDefault(neighbor, int.MaxValue))
                    {
                        _gScores[neighbor] = newGScore;
                        int priority = newGScore + Heuristic(neighbor, goal);
                        _frontier.Enqueue(neighbor, priority);
                        _cameFrom[neighbor] = current;
                    }

                    iteration++;
                    if (iteration > maxIteration) return null;
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
            return _path;
        }
        private List<Vector> GetNeighbours(TileMap tile, Vector position)
        {
            var directions = new Vector[] { Vector.up, Vector.down, Vector.left, Vector.right };
            var neighbors = new List<Vector>();
            foreach (var direction in directions)
            {
                Vector target = position + direction;
                if (target.Y < 0 || target.X < 0 || target.X > tile.Size.X || target.Y > tile.Size.Y) continue;
                if (tile.GetCell(target) == RenderPalette.GetChar(TileType.Wall)) continue;
                neighbors.Add(target);
            }
            return neighbors;
        }

        private int GetCost()
        {
            return 1;
        }

        private int Heuristic(Vector a, Vector b) => Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);


        private bool IsBlocked(char[,] field, Vector pos)
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

        private bool ContainsGoal(Vector pos, Vector goal)
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
