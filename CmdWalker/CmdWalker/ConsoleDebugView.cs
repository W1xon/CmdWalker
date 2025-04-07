namespace CmdWalker
{
    /// <summary>
    /// Временный класс для отображения статистики и отладочной информации в консоли.
    /// Будет заменён на полноценный интерфейс в будущих версиях.
    /// </summary>
    internal static class ConsoleDebugView
    {
        public static (char[][], ConsoleColor[]) InventoryInfo;
        public static string DebugInfo = ""; //удалить в след. версиях
        private static Map _map;
        private static Vector _offset = new Vector(2,2);
        private static int _killCount;

        private static Vector _bound;
        private static int _oldInventoryX = 0, _oldInventoryY = 0;
        public static void SetMap(Map map)
        {
            _bound = map.Size;
            _map = map;
        }
        public static void Show()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;

            Console.SetCursorPosition(_bound.X + _offset.X, _offset.Y);
            Console.Write($"Kills: {_killCount}");

            Console.ForegroundColor = ConsoleColor.White;

            Console.SetCursorPosition(_bound.X + _offset.X, _offset.Y + 3);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write($"Инфа дебага: {DebugInfo}");
            
            
            Console.SetCursorPosition(_bound.X + _offset.X, _offset.Y + 4);
            
            if(InventoryInfo.Item1 != null)
            {
                ClearInventory();
                ShowInventory();
            }

            Console.ForegroundColor = ConsoleColor.White;
        }
        
        public static void AddKill()
        {
            _killCount++;
        }

        private static void ShowInventory()
        {
            for (int y = 0; y < InventoryInfo.Item1.Length; y++)
            {
                Console.ForegroundColor = InventoryInfo.Item2[y / 3];
                for (int x = 0; x < InventoryInfo.Item1[0].Length; x++)
                {
                    int newX = _bound.X + _offset.X + x;
                    int newY = _offset.Y + 4 + y;
                    
                    Console.SetCursorPosition(newX, newY);
                    Console.Write(InventoryInfo.Item1[y][x]);
                }
            }
            
            if(InventoryInfo.Item1.Length > 0)
            {
                _oldInventoryX = InventoryInfo.Item1[0].Length;
                _oldInventoryY = InventoryInfo.Item1.Length;
            }
        }

        private static void ClearInventory()
        {
            for (int y = 0; y < _oldInventoryY; y++)
            {
                for (int x = 0; x < _oldInventoryX; x++)
                {
                    int newX = _bound.X + _offset.X + x;
                    int newY = _offset.Y + 4 + y;
                    
                    Console.SetCursorPosition(newX, newY);
                    Console.Write(' ');
                }
            }
        }
    }
}
