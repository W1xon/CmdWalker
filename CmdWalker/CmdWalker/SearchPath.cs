namespace CmdWalker
{
    internal class SearchPath
    {
        private Map _map;
        private Vector _size;
        public SearchPath(Map map, Vector size)
        {
            _map = map;
            _size = size;
        }

        public List<Vector> GetPath(Vector start, Vector goal)
        {
            PriorityQueue<Vector, int> frontier = new PriorityQueue<Vector, int>();
            frontier.Enqueue(start, 0);

            Dictionary<Vector, Vector> cameFrom = new Dictionary<Vector, Vector>() { { start, default } };
            Dictionary<Vector, int> gScores = new Dictionary<Vector, int>() { { start, 0 } };

            while (frontier.Count > 0)
            {
                var current = frontier.Dequeue();

                if (ContainsGoal(current, goal))
                {
                    goal = current;
                    break;
                }

                foreach (var neighbor in GetNeighbours(current))
                {
                    if (IsBlocked(_map.Carcas, neighbor)) continue;
                    var newGScore = gScores[current] + GetCost();
                    if (!gScores.ContainsKey(neighbor) || newGScore < gScores[neighbor])
                    {
                        gScores[neighbor] = newGScore;
                        var priority = newGScore + Heuristic(neighbor, goal);
                        frontier.Enqueue(neighbor, priority);
                        cameFrom[neighbor] = current;
                    }
                }
            }

            var pos = goal;
            List<Vector> path = new List<Vector>();
            while (pos != start)
            {
                path.Add(pos);
                if (!cameFrom.TryGetValue(pos, out var value)) return null;
                pos = value;
            }
            path.Reverse();
            return path;
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
                if (target.Y < 0 || target.X < 0 || target.X > _map.Size.X || target.Y > _map.Size.Y) continue;
                if (_map.GetCell(target) == Blocks.GetGlyph(Block.Wall)) continue;
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


        public bool IsBlocked(char[][] field, Vector pos)
        {
            for (int x = 0; x < _size.X; x++)
            {
                for (int y = 0; y < _size.Y; y++)
                {
                    int newX = pos.X + x;
                    int newY = pos.Y + y;

                    if (newX < 0 || newX >= field[0].Length ||
                        newY < 0 || newY >= field.Length ||
                        field[newY][newX] == Blocks.GetGlyph(Block.Wall))
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
