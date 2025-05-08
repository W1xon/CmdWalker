namespace CmdWalker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Game.SetFPS(60);
            Game game = new Game();
            game.Start();
        }
    }
}
