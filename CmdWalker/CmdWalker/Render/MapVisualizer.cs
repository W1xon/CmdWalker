namespace CmdWalker;

internal static class MapVisualizer
{
    private static readonly Queue<Vector> _frontier = new();
    private static readonly HashSet<Vector> _visited = [];
    private static readonly List<Vector> _neighbors = new(8);

    public static void AnimateReachableArea(Map map, Vector center, bool fillStaticWalls = false)
    {
        if (fillStaticWalls)
        {
            RenderStaticWallsAnimated(map);
            RenderEntireMapSilent(map);
        }

        _visited.Clear();
        _frontier.Clear();
        _frontier.Enqueue(center);
        _visited.Add(center);

        while (_frontier.Count > 0)
        {
            int levelSize = _frontier.Count;
            for (int i = 0; i < levelSize; i++)
            {
                var current = _frontier.Dequeue();
                map.Draw(current, map.GetCell(current).ToString());

                FindNeighbors(map.Plane, current);
                foreach (var neighbor in _neighbors)
                {
                    if (_visited.Contains(neighbor)) continue;
                    _visited.Add(neighbor);

                    if (map.Carcas.IsFree(neighbor, Vector.One))
                    {
                        _frontier.Enqueue(neighbor);
                    }
                    else
                    {
                        map.Draw(neighbor, map.GetCell(neighbor).ToString());
                    }
                }
            }
            Thread.Sleep(20); 
        }
    }

    private static void RenderStaticWallsAnimated(Map map)
    {
        _visited.Clear();
        _frontier.Clear();

        Vector[] seeds = {
            new(0, 0),
            new(map.Size.X - 1, 0),
            new(0, map.Size.Y - 1),
            new(map.Size.X - 1, map.Size.Y - 1)
        };

        foreach (var s in seeds)
        {
            _frontier.Enqueue(s);
            _visited.Add(s);
        }

        while (_frontier.Count > 0)
        {
            int levelSize = _frontier.Count;
            for (int i = 0; i < levelSize; i++)
            {
                var current = _frontier.Dequeue();
                
                if (!map.Carcas.IsFree(current, Vector.One))
                    map.Draw(current, map.GetCell(current).ToString());

                FindNeighbors(map.Plane, current);
                foreach (var neighbor in _neighbors)
                {
                    if (_visited.Contains(neighbor)) continue;
                    _visited.Add(neighbor);

                    if (!map.Carcas.IsFree(neighbor, Vector.One))
                    {
                        _frontier.Enqueue(neighbor);
                    }
                }
            }
            Thread.Sleep(5); 
        }
    }

    private static void RenderEntireMapSilent(Map map)
    {
        for (int y = 0; y < map.Size.Y; y++)
        for (int x = 0; x < map.Size.X; x++)
        {
            Vector p = new Vector(x, y);
            if (!map.Carcas.IsFree(p, Vector.One))
            {
                map.Draw(p, map.GetCell(p).ToString());
            }
        }
    }

    private static void FindNeighbors(TileMap map, Vector pos)
    {
        _neighbors.Clear();
        int[] dx = { 0, 0, -1, 1, -1, 1, -1, 1 };
        int[] dy = { -1, 1, 0, 0, -1, -1, 1, 1 };

        for (int i = 0; i < 8; i++)
        {
            int nx = pos.X + dx[i];
            int ny = pos.Y + dy[i];
            if (nx >= 0 && nx < map.Size.X && ny >= 0 && ny < map.Size.Y)
                _neighbors.Add(new Vector(nx, ny));
        }
    }
}