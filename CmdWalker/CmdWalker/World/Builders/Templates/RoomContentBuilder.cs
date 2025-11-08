namespace CmdWalker;

internal class RoomContentBuilder(LvlConfig config) : ContentBuilder(config)
{
    private Random _rand = new Random();
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
                        .Create(GetPosition(RenderPalette.GetSize(TileType.Portal)), true));
                    Content.GameEntities.Add(CreatorRegistry.GetCreator<PortalCreator>(entityType)
                        .Create(GetPosition(RenderPalette.GetSize(TileType.Portal)), false));
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
                        CreateOnMap(GetPosition(RenderPalette.GetSize(TileType.BounceBullet))));
                }
                else if (itemType == typeof(Bullet))
                {
                    Content.GameEntities.Add(CreatorRegistry.GetCreator<BulletCreator>(itemType).
                        CreateOnMap(GetPosition(RenderPalette.GetSize(TileType.Bullet))));
                }
                else if (itemType == typeof(Gun))
                {
                    Content.GameEntities.Add(CreatorRegistry.GetCreator<GunCreator>(itemType).
                        CreateOnMap(GetPosition(RenderPalette.GetSize(TileType.Gun))));
                }
            }
            count++;
        }
        Content.GameEntities.Add(CreatorRegistry.GetCreator<GrenadeCreator, Grenade>().
            CreateOnMap(GetPosition(Vector.one)));
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
                        .Create(GetPosition(RenderPalette.GetSize(TileType.Skeleton))));
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
        var player = GameScene.Map.EntityManager.GetEntity<Player>().FirstOrDefault();
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

    private Vector GetPosition(Vector size)
    {
        Vector position = Vector.zero;
        bool isOccupied = true;
        do
        {
            Vector randV = Vector.GetRandom().Abs();
            position.X =  randV.X *_rand.Next(0, _tileMap.Size.X);
            position.Y =  randV.Y *_rand.Next(0, _tileMap.Size.Y);
            if (!_tileMap.IsFree(position, size)) continue;

            var entities = Content.GameEntities ?? Enumerable.Empty<GameEntity>();
            var units = Content.Units ?? Enumerable.Empty<Unit>();
            var items = Content.Items ?? Enumerable.Empty<GameEntity>();

            isOccupied =
                entities.Any(e => Collider.Intersects(e.Transform.Position, e.Transform.Size, position, size)) ||
                units.Any(u => Collider.Intersects(u.Transform.Position, u.Transform.Size, position, size)) ||
                items.Any(i => Collider.Intersects(i.Transform.Position, i.Transform.Size, position, size));



        } while (isOccupied);

        return position;
    }
}