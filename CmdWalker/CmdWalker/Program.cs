namespace CmdWalker
{
    internal class Program
    {
        static void Main(string[] args)
        {
           // Console.SetWindowSize(100, 40);
            Console.CursorVisible = false;
            Game game = new Game();
            game.Start();
        }
    }
}
