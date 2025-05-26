namespace CmdWalker;

internal class SafeZoneContentBuilder(LvlConfig config) : ContentBuilder(config)
{
    public override void AddCarcassBuilder()
    {
        
        Content.CarcassGenerator = new SafeZoneCarcassGenerator(_config);
    }

    public override void AddEntities()
    {
        Content.GameEntities = new List<GameEntity>()
        {
            CreatorRegistry.GetCreator<PortalCreator, Portal>().Create(new Vector(0, 7), true),
        };
    }

    public override void AddItems()
    {
        //достаем инфу из сохранений в дальнейшем
    }

    public override void AddUnits()
    {
        Content.Units = new List<Unit>()
        {
            (Player)CreatorRegistry.GetCreator<PlayerCreator, Player>().Create(new Vector(15,7)),
            (Cat)CreatorRegistry.GetCreator<CatCreator, Cat>().Create(Vector.one)
        };
    }

    public override void SetConfig()
    {
        _config.Size = new Vector(40, 20);
        _config.Configure();
    }
}