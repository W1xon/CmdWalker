namespace CmdWalker;

internal static class SceneManager
{
    private static readonly Stack<IScene> _sceneStack = new();

    public static IScene ActiveScene => _sceneStack.Any() ? _sceneStack.Peek() : null;
    public static void Activate(IScene scene)
    {
        if (scene == null) throw new ArgumentNullException(nameof(scene));
        scene.Enter();
        _sceneStack.Push(scene);
    }
    public static void Activate(GameScene scene, LvlDifficult lvlDifficult)
    {
        if (scene == null) throw new ArgumentNullException(nameof(scene));
        scene.Enter(lvlDifficult);
        _sceneStack.Push(scene);
    }
    public static void Deactivate()
    {
        if (!_sceneStack.Any()) throw new InvalidOperationException("Нет сцен для деактивации.");
        var scene = _sceneStack.Pop();
        scene.Exit();
    }
    public static void SwitchTo(IScene scene)
    {
        if (_sceneStack.Any()) Deactivate();
        Activate(scene);
    }

    public static void SwitchTo(GameScene scene, LvlDifficult lvlDifficult)
    {
        if (_sceneStack.Any()) Deactivate();
        Activate(scene, lvlDifficult);
    }
}