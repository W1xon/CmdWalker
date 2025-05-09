using System.Diagnostics;

namespace CmdWalker;
internal class Game
{
    public static int TargetFPS { get; private set; } = 30;
    public static int CurrentFPS { get; private set; }

    private int _frameCount;
    private readonly Stopwatch _fpsStopwatch = new Stopwatch();

    public static void SetFPS(int fps)
    {
        if (fps > 0)
            TargetFPS = fps;
    }

    public void Start()
    {
        SceneManager.Activate(new MenuScene(new Vector(90, 30)));

        var frameDelay = TimeSpan.FromSeconds(1.0 / TargetFPS);
        DateTime lastFrameTime = DateTime.Now;
        _fpsStopwatch.Start();

        while (true)
        {
            DateTime now = DateTime.Now;
            if (now - lastFrameTime >= frameDelay)
            {
                lastFrameTime = now;
                InputHandler.UpdateInput();
                SceneManager.ActiveScene.Update();
                _frameCount++;
                UpdateFPS();
            }
        }
    }

    private void UpdateFPS()
    {
        if (_fpsStopwatch.Elapsed.TotalSeconds >= 1)
        {
            CurrentFPS = _frameCount;
            _frameCount = 0;
            _fpsStopwatch.Restart();
        }
    }
}