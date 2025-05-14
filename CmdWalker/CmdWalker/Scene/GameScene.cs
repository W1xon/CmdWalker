namespace CmdWalker;

internal abstract class GameScene : IScene
{
    public static Map Map;
    public abstract bool IsActive { get; set; }

    public virtual void Enter()
    {
        IsActive = true;
        Console.Clear();
    }

    public virtual void Update()
    {
        foreach (var entity in Map.Entities)
        {
            entity.Update();
            if (!IsActive)
                return;
        }
    }
    public virtual void Exit()
    {
        IsActive = false;
    }
    public abstract void InitCanvas();
}