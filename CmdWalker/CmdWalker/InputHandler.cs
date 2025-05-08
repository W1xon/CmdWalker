namespace CmdWalker
{
    internal class InputHandler
    {
        private  static ConsoleKey _currentKey;
        public static Vector GetCurrentDirection()
        {
           switch (_currentKey)
           {
               case ConsoleKey.W: return Vector.down;
               case ConsoleKey.S: return Vector.up;
               case ConsoleKey.A: return Vector.left;
               case ConsoleKey.D: return Vector.right;
           }
            return Vector.zero;
        }
        public static ConsoleKey GetKeyDown()
        {
            return _currentKey;
        }
        public static void UpdateInput()
        {
            if (Console.KeyAvailable)
            {
                _currentKey = Console.ReadKey(true).Key;
            }
            else
                //кнопка по уполчанию, чтобы не было зажатий клавиш
                _currentKey = ConsoleKey.Spacebar;
        }
    }
}
