namespace CmdWalker;

internal class RoomGameScene : GameScene
{
    
    private Canvas _canvas = new Canvas();
    private Debug _debug = new Debug();

    public override bool IsActive { get; set; }

    public override void Enter()
    {   
        base.Enter();
        IsActive = true;
        MapBuilder mapBuilder = new MapBuilder();
        MapGenerator mapGenerator = new MapGenerator(mapBuilder);
        LvlConfig lvlConfig = new LvlConfig(LvlDifficult.Easy);
        Map = mapGenerator.Generate(new RoomContentBuilder(lvlConfig));
        
        InitCanvas();
        Map.Show();
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