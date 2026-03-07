namespace CmdWalker;

internal class RoomContentBuilder(LvlConfig config) : ContentBuilder(config)
{
    public override void AddCarcassBuilder()
    {
        Content.CarcassGenerator = new BSPCarcasGenerator(_config);
    }

    public override void AddEntities()                      
    {   
        int count = 0;
        Content.GameEntities = new List<GameEntity>();
        foreach (var (entityType, value) in _config.EntityPreferences)
        {
            for (int i = 0; i < value; i++)
            {

                if (entityType == typeof(Portal))
                {
                    Content.GameEntities.Add(CreatorRegistry.GetCreator<PortalCreator>(entityType)
                        .Create(GetFreePosition(RenderPalette.GetSize(TileType.Portal)), true));
                    Content.GameEntities.Add(CreatorRegistry.GetCreator<PortalCreator>(entityType)
                        .Create(GetFreePosition(RenderPalette.GetSize(TileType.Portal)), false));
                }
            }

            count++;
        }
    }

    public override void AddItems()
    {
        int count = 0;
        Content.Items = new List<GameEntity>();
        foreach (var (itemType, value) in _config.ItemPreferences)
        {
            for (int i = 0; i < value; i++)
            {
                
                if (itemType == typeof(BounceBullet))
                {
                    Content.GameEntities.Add(CreatorRegistry.GetCreator<BounceBulletCreator>(itemType).
                        CreateOnMap(GetFreePosition(RenderPalette.GetSize(TileType.BounceBullet))));
                }
                else if (itemType == typeof(Bullet))
                {
                    Content.GameEntities.Add(CreatorRegistry.GetCreator<BulletCreator>(itemType).
                        CreateOnMap(GetFreePosition(RenderPalette.GetSize(TileType.Bullet))));
                }
                else if (itemType == typeof(Gun))
                {
                    Content.GameEntities.Add(CreatorRegistry.GetCreator<GunCreator>(itemType).
                        CreateOnMap(GetFreePosition(RenderPalette.GetSize(TileType.Gun))));
                }
            }
            count++;
        }
        Content.GameEntities.Add(CreatorRegistry.GetCreator<GrenadeCreator, Grenade>().
            CreateOnMap(GetFreePosition(Vector.One)));
    }

    public override void AddUnits()
    {
        int count = 0;
        
        Content.Units = new List<Unit>();
        foreach (var (unitType, value) in _config.UnitPreferences)
        {
            for (int i = 0; i < value; i++)
            {
                if (unitType != typeof(Skeleton)) continue;
                
                Content.GameEntities.Add(
                    CreatorRegistry.GetCreator<SkeletonCreator>(unitType)
                        .Create(GetFreePosition(RenderPalette.GetSize(TileType.Skeleton))));
            }

            count++;
        }
        /*
        Content.GameEntities.Add(
            CreatorRegistry.GetCreator<SkeletonCreator, Skeleton>()
                .Create(GetPosition(RenderPalette.GetSize(TileType.Skeleton))));
        Content.GameEntities.Add(
            CreatorRegistry.GetCreator<SkeletonCreator, Skeleton>()
                .Create(GetPosition(RenderPalette.GetSize(TileType.Skeleton))));*/
        var player = GameScene.Map.EntityManager.GetPlayer();
        var playerPos = Content.GameEntities.First(e => e is Portal { IsEntrance: false }).Transform.Position;
        if (player != null)
        {
            player.Transform.Position = playerPos;
            Content.GameEntities.Add(player);
        }
        else
        {
            Content.GameEntities.Add((Player)CreatorRegistry
                .GetCreator<PlayerCreator, Player>()
                .Create(playerPos));
        }
    }

    public override void SetConfig()
    {
        _config.MaxRoomSize = new Vector(50,20);
        _config.MinRoomSize = new Vector(20,10);
        _config.Size = new Vector(80, 30);
        _config.Configure();
    }
    public override void AddConstruction()
    {
    }
}