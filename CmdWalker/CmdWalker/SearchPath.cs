namespace CmdWalker
{
    internal class SearchPath
    {
        private Vector _size;
        private readonly Dictionary<Vector, Vector> _cameFrom = new();
        private readonly Dictionary<Vector, int> _gScores = new();
        private readonly PriorityQueue<Vector, int> _frontier = new();
        private readonly List<Vector> _path = new();
        
        private static readonly Vector[] _directions = new[]
        {
            Vector.up,
            Vector.down,
            Vector.left,
            Vector.right
        };


        public SearchPath(Vector size)
        {
            _size = size;
        }

        public Vector GetNextPosition(TileMap tile, Vector start, Vector goal, int maxIteration)
        {
            if (!_path.Contains(start) && !_path.Contains(goal))
                BuildPath(tile, start, goal, maxIteration);
            if (_path == null || _path.Count == 0) return Vector.zero;
            return _path[0];
        }

        private void BuildPath(TileMap tile, Vector start, Vector goal, int maxIteration)
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
                    var newGScore = _gScores[current] + 1; 

                    if (!_gScores.TryGetValue(neighbor, out var existingScore) || newGScore < existingScore)
                    {
                        _gScores[neighbor] = newGScore;
                        int priority = newGScore + Heuristic(neighbor, goal);
                        _frontier.Enqueue(neighbor, priority);
                        _cameFrom[neighbor] = current;
                    }
                    iteration++;
                    if (iteration > maxIteration)
                    {
                        _path.Clear();
                        return;
                    }
                }
            }

            var pos = goal;
            while (pos != start)
            {
                _path.Add(pos);
                if (!_cameFrom.TryGetValue(pos, out var prev))
                {
                    _path.Clear();
                    return;
                }
                pos = prev;
            }
            _path.Reverse();
        }

        private List<Vector> GetNeighbours(TileMap tile, Vector position)
        {
            var neighbors = new List<Vector>(4); 
            foreach (var direction in _directions)
            {
                int newX = position.X + direction.X;
                int newY = position.Y + direction.Y;
                
                if (newY < 0 || newX < 0 || newX > tile.Size.X || newY > tile.Size.Y) continue;
                if (tile.GetCell(new Vector(newX, newY)) == RenderPalette.GetChar(TileType.Wall)) continue;
                
                neighbors.Add(new Vector(newX, newY));
            }
            return neighbors;
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
                    
                    if (newX == goal.X && newY == goal.Y)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
