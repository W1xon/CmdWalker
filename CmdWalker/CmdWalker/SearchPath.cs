namespace CmdWalker;

internal class SearchPath
{
    private List<Vector> _path = new();
    private Vector _lastTarget = Vector.Zero;

    private static readonly Vector[] _directions = new[] { 
        Vector.Up, Vector.Down, Vector.Left, Vector.Right 
    };

    public Vector GetNextPosition(Map map, Vector start, Vector target, int maxIteration)
    {
        if (_path.Count == 0 || _lastTarget != target || !_path.Contains(start))
        {
            CalculatePath(map, start, target, maxIteration);
            _lastTarget = target;
        }

        if (_path.Count == 0) return start;

        int currentIndex = _path.IndexOf(start);

        if (currentIndex >= 0 && currentIndex < _path.Count - 1)
        {
            return _path[currentIndex + 1];
        }

        return start;
    }

    private void CalculatePath(Map map, Vector start, Vector goal, int maxIteration)
    {
        _path.Clear();
            
        var cameFrom = new Dictionary<Vector, Vector>();
        var costSoFar = new Dictionary<Vector, int>();
        var frontier = new PriorityQueue<Vector, int>();

        frontier.Enqueue(start, 0);
        cameFrom[start] = start; 
        costSoFar[start] = 0;

        int iterations = 0;

        while (frontier.Count > 0)
        {
            iterations++;
            if (iterations > maxIteration) break; 

            var current = frontier.Dequeue();

            if (current == goal) break; 

            foreach (var next in GetNeighbors(map, current))
            {
                int newCost = costSoFar[current] + 1; 

                if (!costSoFar.ContainsKey(next) || newCost < costSoFar[next])
                {
                    costSoFar[next] = newCost;
                    int priority = newCost + Heuristic(next, goal);
                    frontier.Enqueue(next, priority);
                    cameFrom[next] = current;
                }
            }
        }

        Vector endNode = cameFrom.ContainsKey(goal) ? goal : Vector.Zero;
            
        if (endNode == Vector.Zero) return; 

        var temp = endNode;
        while (temp != start)
        {
            _path.Add(temp);
            temp = cameFrom[temp];
        }
        _path.Add(start); 
        _path.Reverse();  
    }

    private IEnumerable<Vector> GetNeighbors(Map map, Vector pos)
    {
        foreach (var dir in _directions)
        {
            Vector next = pos + dir;
                
            if (next.X < 0 || next.Y < 0 || next.X >= map.Size.X || next.Y >= map.Size.Y) 
                continue;

            if (!map.Carcas.IsFree(next, Vector.One)) 
                continue;

            yield return next;
        }
    }

    private int Heuristic(Vector a, Vector b)
    {
        return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
    }
}