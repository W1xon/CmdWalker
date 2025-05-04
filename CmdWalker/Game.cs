namespace CmdWalker
{
    internal class Game
    {
        public void Start()
        {        
            SceneManager.Activate(new MenuScene(new Vector(90,30)));
            while (true)
            {
                Thread.Sleep(10);
                InputHandler.UpdateInput();
                SceneManager.ActiveScene.Update();
            }
        }
    }
}
