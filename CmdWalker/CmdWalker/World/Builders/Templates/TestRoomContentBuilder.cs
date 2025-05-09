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
        
        Content.Items.Add(CreatorRegistry.GetCreator<BounceBulletCreator,BounceBullet>().CreateOnMap(new Vector(30,7)));
        Content.Items.Add(CreatorRegistry.GetCreator<BounceBulletCreator,BounceBullet>().CreateOnMap(new Vector(30,8)));
        Content.Items.Add(CreatorRegistry.GetCreator<BounceBulletCreator,BounceBullet>().CreateOnMap(new Vector(30,6)));
        Content.Items.Add(CreatorRegistry.GetCreator<BounceBulletCreator,BounceBullet>().CreateOnMap(new Vector(30,9)));
        Content.Items.Add(CreatorRegistry.GetCreator<BounceBulletCreator,BounceBullet>().CreateOnMap(new Vector(30,5)));
        
        Content.Items.Add(CreatorRegistry.GetCreator<GunCreator, GunCreator>().CreateOnMap(new Vector(20,7)));
    }

    public override void AddUnits()
    {
        Content.Units = new List<Unit>();
        Content.Units =
        [
            (Player)CreatorRegistry.GetCreator<PlayerCreator, Player>().Create(new Vector(15, 7))
        ];
    }

    public override void SetConfig()
    {
        _config.Size = new Vector(90, 15);
        _config.Configure();
    }
}