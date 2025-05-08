namespace CmdWalker;

internal class SafeZoneScene : GameScene
{
    public override bool IsActive { get; set; }
    private Canvas _canvas = new Canvas();
    public override void Enter()
    {
        base.Enter();
        IsActive = true;
        MapBuilder mapBuilder = new MapBuilder();
        MapGenerator mapGenerator = new MapGenerator(mapBuilder);
        LvlConfig lvlConfig = new LvlConfig(LvlDifficult.Easy);
        Map = mapGenerator.Generate(new SafeZoneContentBuilder(lvlConfig));
        
        InitCanvas();
        Map.Show();
    }

    public override void InitCanvas()
    {
        _canvas.AddChild(Map, Vector.zero);
    }
}