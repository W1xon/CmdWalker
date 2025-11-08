using System.Diagnostics.CodeAnalysis;

namespace CmdWalker;

internal static class MapVisualizer
{
    
    private static List<Vector> _reachableCells = new List<Vector>();
    private static Queue<Vector> _frontier = new Queue<Vector>();
    private static HashSet<Vector> _visited = new HashSet<Vector>();
    
    private static List<Vector> _neighbors = new List<Vector>();
    public static void RenderEntireMap(Map map)
    {
        for (int y = 0; y < map.Size.Y; y++)
        {
            for (int x = 0; x < map.Size.X; x++)
            {
                Vector pos = new Vector(x, y);
                map.Draw(pos, map.GetCell(pos).ToString(), map );
            }
            Console.WriteLine();
        }
    }

   public static void AnimateReachableArea(Map map, Vector center)
         {
             
             _reachableCells.Clear();
             _frontier.Clear();
             _visited.Clear();
             _frontier.Clear();
     
             _frontier.Enqueue(center);
             _visited.Add(center);
     
             while (_frontier.Count > 0)
             {
                 var current = _frontier.Dequeue();
                 FindNeighbors(map.Plane, current);
                 Thread.Sleep(TimeSpan.FromTicks(100));
                 map.Draw(current, map.GetCell(current).ToString(), map);
                 foreach (var neighbor in _neighbors)
                 {
                     if (_visited.Contains(neighbor))
                         continue;
     
                     _frontier.Enqueue(neighbor);
                     _visited.Add(neighbor);
     
                     _reachableCells.Add(neighbor);
                 }
             }
         }
         private static void FindNeighbors(TileMap map, Vector pos)
         {
             _neighbors.Clear();
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
                     _neighbors.Add(newPos);
                 }
             }
         }
}