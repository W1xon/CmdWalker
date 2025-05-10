using System.Diagnostics;
namespace CmdWalker;

internal static class Timing
{
    private class TimingData
    {
        public int CallCount { get; set; }
        public double TotalMilliseconds { get; set; }
    }

    private static Dictionary<GameEntity, TimingData> _timings = new Dictionary<GameEntity, TimingData>();
    private static Stopwatch _stopwatch = new Stopwatch();

    public static void Start(GameEntity entity)
    {
        if (_timings.TryGetValue(entity, out var data))
        {
            data.CallCount++;
        }
        else
        {
            _timings.TryAdd(entity, new TimingData { CallCount = 1, TotalMilliseconds = 0 });
        }
        _stopwatch.Restart();
    }

    public static void Stop(GameEntity entity)
    {
        _stopwatch.Stop();
        if (_timings.TryGetValue(entity, out var data))
        {
            data.TotalMilliseconds += _stopwatch.Elapsed.TotalMilliseconds;
        }
    }

    public static Dictionary<GameEntity, double> CalculateAverageTime()
    {
        var averageTimes = new Dictionary<GameEntity, double>();
        foreach (var entity in _timings.Keys)
        {
            var data = _timings[entity];
            double average = (data.CallCount > 0 && data.TotalMilliseconds > 0) 
                ? data.TotalMilliseconds / data.CallCount 
                : 0;
            averageTimes.Add(entity, average);
        }
        return averageTimes;
    }
}