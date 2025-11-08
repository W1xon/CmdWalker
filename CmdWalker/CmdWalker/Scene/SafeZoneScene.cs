namespace CmdWalker;

internal class SafeZoneScene : GameScene
{
    public override void Enter(LvlDifficult lvlDifficult)
    {
        base.Enter();
        IsActive = true;
        LvlConfig lvlConfig = new LvlConfig(lvlDifficult);
        Map = _mapGenerator.Generate(new SafeZoneContentBuilder(lvlConfig));
        InitCanvas();
        MapVisualizer.RenderEntireMap(Map);
    }
    public override void Update()
    {
        base.Update();
        if (!IsActive) return;
        Debug.Show();
    }
    public override void InitCanvas()
    {
        _canvas.AddChild(Map, Vector.zero);
        _canvas.AddChild(_debug, new Vector(Map.Size.X + 2, 0));
    }
}