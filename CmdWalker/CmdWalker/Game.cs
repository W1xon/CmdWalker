namespace CmdWalker;
internal class Game
    {
        public static int TargetFPS { get; private set; } = 30;

        public static void SetFPS(int fps)
        {
            if (fps > 0)
            {
                TargetFPS = fps;
            }
        }

        public void Start()
        {
            SceneManager.Activate(new MenuScene(new Vector(90, 30)));
            
            double targetFrameTime = 1000.0 / TargetFPS;
            DateTime lastFrameTime = DateTime.Now;

            while (true)
            {
                if (HandleFrameTiming(ref targetFrameTime, ref lastFrameTime))
                {
                    InputHandler.UpdateInput();
                    SceneManager.ActiveScene.Update();
                }
            }
        }

        private bool HandleFrameTiming(ref double targetFrameTime, ref DateTime lastFrameTime)
        {
            DateTime currentTime = DateTime.Now;
            double elapsedTime = (currentTime - lastFrameTime).TotalMilliseconds;

            if (elapsedTime >= targetFrameTime)
            {
                lastFrameTime = currentTime;

                double sleepTime = targetFrameTime - elapsedTime;
                if (sleepTime > 0)
                {
                    Thread.Sleep((int)sleepTime);
                }

                targetFrameTime = 1000.0 / TargetFPS;
                return true; 
            }

            return false; 
        }
    }