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
        reachableCells.Add(start);

        while (frontier.Count > 0)
        {
            var current = frontier.Dequeue();

            if (radius != 0 && Vector.Distance(start, current) > radius)
                continue;

            foreach (var neighbor in GetNeighbors(map, current))
            {
                if (!visited.Add(neighbor))
                    continue;

                frontier.Enqueue(neighbor);
                reachableCells.Add(neighbor);
            }
        }

        return reachableCells;
    }

    private static IEnumerable<Vector> GetNeighbors(TileMap map, Vector pos)
    {
        int[] dx = { 0, 0, -1, 1 };
        int[] dy = { -1, 1, 0, 0 };

        for (int i = 0; i < 4; i++)
        {
            var newPos = new Vector(pos.X + dx[i], pos.Y + dy[i]);

            if (!map.IsWithinBounds(newPos))
                continue;

            if (map.GetCell(newPos) == RenderPalette.GetChar(TileType.Wall))
                continue;

            yield return newPos;
        }
    }
}