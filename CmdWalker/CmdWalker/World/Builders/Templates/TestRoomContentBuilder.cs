namespace CmdWalker;

internal class TestRoomContentBuilder(LvlConfig config) : ContentBuilder(config)
{
    public override void AddCarcassBuilder()
    {
        
        Content.CarcassGenerator = new SafeZoneCarcassGenerator(_config);
    }

    public override void AddEntities()
    {
    }

    public override void AddItems()
    {
        Content.Items = new List<GameEntity>();
        
        Content.Items.Add(CreatorRegistry.GetCreator<BulletCreator,Bullet>().CreateOnMap(new Vector(30,7)));
        Content.Items.Add(CreatorRegistry.GetCreator<BulletCreator,Bullet>().CreateOnMap(new Vector(30,8)));
        Content.Items.Add(CreatorRegistry.GetCreator<BulletCreator,Bullet>().CreateOnMap(new Vector(30,6)));
        
        Content.Items.Add(CreatorRegistry.GetCreator<GunCreator,Gun>().CreateOnMap(new Vector(25,7)));
        
    }

    public override void AddUnits()
    {
        Content.Units = new List<Unit>();
        Content.Units =
        [
            (Player)CreatorRegistry.GetCreator<PlayerCreator, Player>().Create(new Vector(15, 7)),
            (Skeleton)CreatorRegistry.GetCreator<SkeletonCreator, Skeleton>().Create(new Vector(35,20)),
        ];
    }

    public override void SetConfig()
    {
        _config.Size = new Vector(90, 30);
        _config.Configure();
    }
}