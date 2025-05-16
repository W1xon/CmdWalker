namespace CmdWalker;

internal class ReachableZoneScanner
{
    public static List<Vector> GetReachableCells(TileMap map, Vector start, int radius = 0)
    {
        var reachableCells = new List<Vector>();
        var frontier = new Queue<Vector>();
        var visited = new HashSet<Vector>();

        frontier.Enqueue(start);
        visited.Add(start);

        while (frontier.Count > 0)
        {
            var current = frontier.Dequeue();
            if (radius != 0 && Vector.Distance(start, current) > radius)
                break;
            var neighbors = GetNeighbors(map, current);
        
            foreach (var neighbor in neighbors)
            {
                if (visited.Contains(neighbor))
                    continue;

                frontier.Enqueue(neighbor);
                visited.Add(neighbor);

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