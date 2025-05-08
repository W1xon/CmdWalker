namespace CmdWalker;

public static class ListExtensions
{
    private static Random _random = new Random();
    public static T GetRandomValue<T>(this List<T> source)
    {
        int index = _random.Next(0, source.Count);
        return source[index];
    }
}