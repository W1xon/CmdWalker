namespace CmdWalker;

internal class GameScene : IScene
{
    public static Map map;
    private Canvas _canvas = new Canvas();
    private ConsoleDebugView _consoleDebugView = new ConsoleDebugView();
    public bool IsActive { get; set; }
    
    public void Enter()
    {   
        IsActive = true;
        MapBuilder mapBuilder = new MapBuilder();
        MapGenerator mapGenerator = new MapGenerator(mapBuilder);
        var template = CreateTemplate();
        map = mapGenerator.Generate(template, new BSPCarcasGenerator(template));
        
        InitCanvas();
        map.Show();
        
        ConsoleDebugView.SetMap(map);
    }

    public void Update()
    {
        foreach (var entity in map.Entities)
        {
            if (!IsActive) return;
            entity.Update();
        }
        ConsoleDebugView.Show();
    }

    public void Exit()
    {
        IsActive = false;
        Console.Clear();
    }

    public MapTemplate CreateTemplate()
    {
        var mapTemplate = new MapTemplate();
        mapTemplate.MaxRoomSize = 20;
        mapTemplate.Size = new Vector(80, 30);
        mapTemplate.GameEntities = new List<GameEntity>()
        {
            CreatorRegistry.GetCreator<PortalCreator, Portal>().Create(new Vector(0, 10)),
        };
        mapTemplate.Units = new List<Unit>()
        {
            (Player)CreatorRegistry.GetCreator<PlayerCreator, Player>().Create(new Vector(3, 5)),
            (Skeleton)(CreatorRegistry.GetCreator<SkilletCreator, Skeleton>().Create(new Vector(60, 5))),
            (Skeleton)(CreatorRegistry.GetCreator<SkilletCreator, Skeleton>().Create(new Vector(70, 15))),
        };
        mapTemplate.Items = new List<GameEntity>()
        {
            CreatorRegistry.GetCreator<BulletCreator, Bullet>().CreateOnMap(new Vector(10, 10)),
            CreatorRegistry.GetCreator<BulletCreator, Bullet>().CreateOnMap(new Vector(20, 19)),
            CreatorRegistry.GetCreator<BulletCreator, Bullet>().CreateOnMap(new Vector(39, 1)),
            CreatorRegistry.GetCreator<BulletCreator, Bullet>().CreateOnMap(new Vector(40, 1)),
            CreatorRegistry.GetCreator<BulletCreator, Bullet>().CreateOnMap(new Vector(41, 1)),
            CreatorRegistry.GetCreator<BulletCreator, Bullet>().CreateOnMap(new Vector(70, 6)),
            CreatorRegistry.GetCreator<GunCreator, Gun>().CreateOnMap(new Vector(4, 6)),
        };
        return mapTemplate;
    }

    private void InitCanvas()
    {
        _canvas.AddChild(map, Vector.zero);
        _canvas.AddChild(_consoleDebugView, new Vector(map.Size.X + 2, 0));
    }
}