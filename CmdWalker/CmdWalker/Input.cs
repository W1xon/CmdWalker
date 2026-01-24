namespace CmdWalker;

public static class Input
{
    private static ConsoleKey _currentKey;
    private static Dictionary<ConsoleKey, Action> _bindings = new();

    public static void Bind(ConsoleKey key, Action action)
    {
        if (_bindings.ContainsKey(key))
            _bindings[key] += action;
        else
            _bindings[key] = action;
    }

    public static void Unbind(ConsoleKey key, Action action)
    {
        if (_bindings.ContainsKey(key))
            _bindings[key] -= action;
    }
    public static ConsoleKey GetKeyDown()
    {
        return _currentKey;
    }  
    public static void UpdateInput()
    {
        _currentKey = Console.KeyAvailable ? Console.ReadKey(true).Key :
            //кнопка по уполчанию, чтобы не было зажатий клавиш
            ConsoleKey.Spacebar;
            
        if (_bindings.TryGetValue(_currentKey, out var action))
            action?.Invoke();
            
    }
}