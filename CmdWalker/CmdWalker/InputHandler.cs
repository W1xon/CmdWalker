namespace CmdWalker
{
    internal class InputHandler
    {
        private static ConsoleKey _currentKey;
        public static Vector GetCurrentDirection()
        {
           switch (_currentKey)
           {
               case ConsoleKey.W: return Vector.Down;
               case ConsoleKey.S: return Vector.Up;
               case ConsoleKey.A: return Vector.Left;
               case ConsoleKey.D: return Vector.Right;
           }
            return Vector.Zero;
        }   
        public static ConsoleKey GetKeyDown()
        {
            return _currentKey;
        }   
        public static void UpdateInput()
        {
            _currentKey = Console.KeyAvailable ? Console.ReadKey(true).Key :
                //кнопка по уполчанию, чтобы не было зажатий клавиш
                ConsoleKey.Spacebar;
        }
    }
}
