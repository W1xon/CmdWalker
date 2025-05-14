namespace CmdWalker
{
    internal class Program
    {
        static void Main()
        {
            Console.CursorVisible = false;
            Game.SetFPS(30);
            Game game = new Game();
            game.Start();
        }
    }
}
