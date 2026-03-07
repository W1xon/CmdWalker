namespace CmdWalker;

internal class SearchPath
{
    private List<Vector> _path = new();
    private Vector _lastTarget = Vector.Zero;
    private readonly Random _rnd = new Random();
    private static readonly Vector[] _directions = new[] { 
        Vector.Up, Vector.Down, Vector.Left, Vector.Right 
    };

    public Vector GetNextPosition(Map map, Vector start, Vector target, 
        Vector size = default, int maxIteration = 100)
    {
        if (_path.Count == 0 || _lastTarget != target || start != _path[0])
        {
            CalculatePath(map, start, target,size, maxIteration);
            _lastTarget = target;
        }
        if (_path.Count == 0) return Vector.Zero;


        if (_path.Count > 1) 
        {
            return _path[1]; 
        }
        return Vector.Zero;
    }

    private void CalculatePath(Map map, Vector start, Vector goal, 
        Vector size = default, int maxIteration = 100)
    {
            
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

            foreach (var next in GetNeighbors(map, current, size))
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
        
        _path.Clear();
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

    private IEnumerable<Vector> GetNeighbors(Map map, Vector pos, Vector size)
    {
        foreach (var dir in _directions)
        {
            Vector next = pos + dir;
                
            if (next.X < 0 || next.Y < 0 || next.X >= map.Size.X || next.Y >= map.Size.Y) 
                continue;
            if(size == default) size = Vector.One;
            if (!map.Carcas.IsFree(next, size)) 
                continue;

            yield return next;
        }
    }

    private int Heuristic(Vector a, Vector b)
    {
        int baseDist = Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
        return baseDist + _rnd.Next(0, 3); 
    }
}