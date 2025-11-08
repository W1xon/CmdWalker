namespace CmdWalker;

internal class ReachableZoneScanner
{
    
    private static Queue<Vector> _frontier = new Queue<Vector>();
    private static HashSet<Vector> _visited = new HashSet<Vector>();
    public static List<Vector> GetReachableCells(TileMap map, Vector start, int radius = 0)
    {
        List<Vector> reachableCells = new List<Vector>();
        _frontier.Clear();
        _visited.Clear();
        _frontier.Clear();
        _frontier.Enqueue(start);
        _visited.Add(start);

        while (_frontier.Count > 0)
        {
            var current = _frontier.Dequeue();
            if (radius != 0 && Vector.Distance(start, current) > radius)
                break;
            var neighbors = GetNeighbors(map, current);
        
            foreach (var neighbor in neighbors)
            {
                if (_visited.Contains(neighbor))
                    continue;

                _frontier.Enqueue(neighbor);
                _visited.Add(neighbor);

                reachableCells.Add(neighbor);
            }
        }

        return reachableCells;
    }
    
    private static List<Vector> GetNeighbors(TileMap map, Vector pos)
    {
        var neighbors = new List<Vector>();

        int[] dx = { 0, 0, -1, 1 };
        int[] dy = { -1, 1, 0, 0 };

        for (int i = 0; i < 4; i++)
        {
            int newX = pos.X + dx[i];
            int newY = pos.Y + dy[i];
            Vector newPos = new Vector(newX, newY);
            if (newX >= 0 && newX < map.Size.X &&
                newY >= 0 && newY < map.Size.Y &&
                map.GetCell(newPos) != RenderPalette.GetChar(TileType.Wall))
            {
                neighbors.Add(newPos);
            }
        }

        return neighbors;
    }
}