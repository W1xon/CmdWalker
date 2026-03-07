using System.Diagnostics;

namespace CmdWalker;
internal class Game
{
    public static int TargetFPS { get; private set; }
    public static int CurrentFPS { get; private set; }

    private int _frameCount;
    private readonly Stopwatch _fpsStopwatch = new();

    public static void SetFPS(int fps)
    {
        if (fps > 0)
            TargetFPS = fps;
    }
    
    public void Start()
    {
        SceneManager.Activate(new MenuScene(new Vector(90, 30)));
        double targetTicksPerFrame = Stopwatch.Frequency / (double)TargetFPS;
        long nextFrameTick = Stopwatch.GetTimestamp();
        
        _fpsStopwatch.Start();
    
        while (true)
        {
            long currentTick = Stopwatch.GetTimestamp();
    
            if (currentTick >= nextFrameTick)
            {
                if (currentTick - nextFrameTick > (long)targetTicksPerFrame * 5) 
                {
                    nextFrameTick = currentTick;
                }

                nextFrameTick += (long)targetTicksPerFrame;
        
                Input.UpdateInput();
                SceneManager.ActiveScene.Update();
        
                _frameCount++;
                UpdateFPS();
            }
            else
            {
                Thread.Sleep(1); 
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