namespace CmdWalker;

internal static class CreatorRegistry
{
    
    private static readonly Dictionary<Type, ICreator> _creators = new();

    public static void Init<TCreator, TItem>()
        where TCreator : ICreator, new()
    {
        GetOrCreate<TCreator>(typeof(TItem), () => new TCreator());
    }
    public static TCreator GetCreator<TCreator, TItem>()
        where TCreator : ICreator, new()
    {
        return GetOrCreate<TCreator>(typeof(TItem), () => new TCreator());
    }

    public static T GetCreator<T>(Type itemType) where T : ICreator
    {
        return GetOrCreate<T>(itemType, () => (T)Activator.CreateInstance(typeof(T)));
    }
    private static T GetOrCreate<T>(Type itemType, Func<T> factory) where T : ICreator
    {
        if (_creators.TryGetValue(itemType, out var creator) && creator is T typed)
            return typed;

        var instance = factory();
        _creators[itemType] = instance;
        return instance;
    }
    
}