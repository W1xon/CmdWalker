namespace CmdWalker
{
    /// <summary>
    /// Временный класс для отображения статистики и отладочной информации в консоли.
    /// Будет заменён на полноценный интерфейс в будущих версиях.
    /// </summary>
    internal static class ConsoleDebugView
    {
        public static string DebugInfo = ""; //удалить в след. версиях
        private static Map _map;
        private static Vector _offset = new Vector(2,2);
        private static int _killCount;
        public static void SetMap(Map map)
        {
            _map = map;
        }
        public static void Show()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;

            var bound = _map.Size;
            Console.SetCursorPosition(_offset.X, bound.Y + _offset.Y);
            Console.Write($"Kills: {_killCount}");

            Console.ForegroundColor = ConsoleColor.White;

            Console.SetCursorPosition(_offset.X, bound.Y + _offset.Y + 3);

            Console.ForegroundColor = ConsoleColor.DarkGreen;

            Console.Write($"Инфа дебага: {DebugInfo}");

            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void AddKill()
        {
            _killCount++;
        }
    }
}
