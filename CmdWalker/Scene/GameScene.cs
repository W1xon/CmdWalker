namespace CmdWalker;

internal abstract class GameScene : IScene
{
    public static Map Map;
    public abstract bool IsActive { get; set; }
    public abstract void Enter();

    public virtual void Update()
    {
        foreach (var entity in Map.Entities)
        {
            if (!IsActive) return;
            entity.Update();
        }
    }

    public virtual void Exit()
    {
        IsActive = false;
        Console.Clear();
    }
    public abstract void InitCanvas();
}