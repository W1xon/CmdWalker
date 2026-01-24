namespace CmdWalker;

internal class RoomGameScene : GameScene
{
    public override void Enter(LvlDifficult lvlDifficult)
    {   
        base.Enter();
        IsActive = true;
        LvlConfig lvlConfig = new LvlConfig(lvlDifficult);
        Map = _mapGenerator.Generate(new RoomContentBuilder(lvlConfig));
       InitCanvas();
       MapVisualizer.AnimateReachableArea(Map, Map.EntityManager.GetEntity<Player>().First().Transform.Position);

     }

    public override void Update()
    {
        base.Update();
        if (!IsActive) return;
        Debug.Show();
        
        Render();
    }
    public override void InitCanvas()
    {
        Vector sizeCanvas = new Vector(Map.Size.X + 2, 0) + Map.Size;
        _canvas = new Canvas(sizeCanvas);
        _canvas.AddChild(Map, Vector.Zero);
        _canvas.AddChild(_debug, new Vector(Map.Size.X + 2, 0));
    }
}