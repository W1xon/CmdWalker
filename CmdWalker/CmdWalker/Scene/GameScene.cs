namespace CmdWalker;

public abstract class GameScene : IScene
{
    public static Map Map;
    protected MapBuilder _mapBuilder;
    protected MapGenerator _mapGenerator;
    protected Canvas _canvas;
    protected Debug _debug = new Debug();
    public bool IsActive { get; set; }

    public virtual void Enter()
    {   
        IsActive = true;
        Console.Clear();
       _mapBuilder = new MapBuilder();
       _mapGenerator = new MapGenerator(_mapBuilder);
    }
    public abstract void Enter(LvlDifficult lvlDifficult);
    public virtual void Update()
    {
        foreach (var entity in Map.EntityManager.Entities)
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

    public void Render()
    {
        _canvas.Render();
    }
}