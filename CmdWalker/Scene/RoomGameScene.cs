namespace CmdWalker;

internal class RoomGameScene : GameScene
{
    
    private Canvas _canvas = new Canvas();
    private ConsoleDebugView _consoleDebugView = new ConsoleDebugView();

    public override bool IsActive { get; set; }

    public override void Enter()
    {   
        IsActive = true;
        MapBuilder mapBuilder = new MapBuilder();
        MapGenerator mapGenerator = new MapGenerator(mapBuilder);
        var template = mapGenerator.GenerateTemplate(new MoreItemTemplateBuilder());
        Map = mapGenerator.Generate(template, new BSPCarcasGenerator(template));
        
        InitCanvas();
        Map.Show();
        
        ConsoleDebugView.SetMap(Map);
    }

    public override void Update()
    {
        base.Update();
        ConsoleDebugView.Show();
    }

    public override void InitCanvas()
    {
        _canvas.AddChild(Map, Vector.zero);
        _canvas.AddChild(_consoleDebugView, new Vector(Map.Size.X + 2, 0));
    }
}