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
    }
    public override void InitCanvas()
    {
        _canvas.AddChild(Map, Vector.Zero);
        _canvas.AddChild(_debug, new Vector(Map.Size.X + 2, 0));
    }
}