namespace CmdWalker;

internal class MapVisualizer
{
    private Map _map;
    public MapVisualizer(Map map)
    {
        _map = map;
    }

    public void RenderEntireMap()
    {
        for (int y = 0; y < _map.Size.Y; y++)
        {
            for (int x = 0; x < _map.Size.X; x++)
            {
                Vector pos = new Vector(x, y);
                _map.Draw(pos, _map.GetCell(pos).ToString(), _map );
            }
            Console.WriteLine();
        }
    }

    public void AnimateReachableArea(Vector center)
    {
        var reachableCells = new List<Vector>();
        var frontier = new Queue<Vector>();
        var visited = new HashSet<Vector>();

        frontier.Enqueue(center);
        visited.Add(center);

        while (frontier.Count > 0)
        {
            var current = frontier.Dequeue();
            var neighbors = GetNeighbors(_map.Plane, current);
            Thread.Sleep(TimeSpan.FromTicks(100));
            _map.Draw(current, _map.GetCell(current).ToString(), _map);
            foreach (var neighbor in neighbors)
            {
                if (visited.Contains(neighbor))
                    continue;

                frontier.Enqueue(neighbor);
                visited.Add(neighbor);

                reachableCells.Add(neighbor);
            }
        }
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
                newY >= 0 && newY < map.Size.Y)
            {
                neighbors.Add(newPos);
            }
        }

        return neighbors;
    }
}