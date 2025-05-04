namespace CmdWalker;

internal class SafeZoneScene : GameScene
{
    public override bool IsActive { get; set; }
    private Canvas _canvas = new Canvas();
    public override void Enter()
    {
        IsActive = true;
        
        MapBuilder mapBuilder = new MapBuilder();
        MapGenerator mapGenerator = new MapGenerator(mapBuilder);
        MapTemplate template = mapGenerator.GenerateTemplate(new SafeZoneTemplateBuilder());
        Map = mapGenerator.Generate(template, new SafeZoneCarcassGenerator(template));
        
        InitCanvas();
        Map.Show();
    }

    public override void InitCanvas()
    {
        _canvas.AddChild(Map, Vector.zero);
    }
}