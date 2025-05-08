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
                        .Create(GetPosition(), true));
                    Content.GameEntities.Add(CreatorRegistry.GetCreator<PortalCreator>(entityType)
                        .Create(GetPosition(), false));
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
                    Content.GameEntities.Add(CreatorRegistry.GetCreator<BounceBulletCreator>(itemType).CreateOnMap(GetPosition()));
                }
                else if (itemType == typeof(Bullet))
                {
                    Content.GameEntities.Add(CreatorRegistry.GetCreator<BulletCreator>(itemType).CreateOnMap(GetPosition()));
                }
                else if (itemType == typeof(Gun))
                {
                    Content.GameEntities.Add(CreatorRegistry.GetCreator<GunCreator>(itemType).CreateOnMap(GetPosition()));
                }
            }
            count++;
        }
    }

    public override void AddUnits()
    {
        int count = 0;
        
        Content.Units = new List<Unit>();
        foreach (var (unitType, value) in _config.UnitPreferences)
        {
            for (int i = 0; i < value; i++)
            {

                if (unitType == typeof(Skeleton))
                {
                    Content.GameEntities.Add(
                        CreatorRegistry.GetCreator<SkeletonCreator>(unitType).Create(GetPosition()));
                }
            }

            count++;
        }
        
        var player = GameScene.Map.GetEntity<Player>().FirstOrDefault();
        var playerPos = GetPosition();
        if (player != null)
        {
            player.Position = playerPos;
            Content.GameEntities.Add(player);
        }
        else
        {
            Content.GameEntities.Add((Player)CreatorRegistry.GetCreator<PlayerCreator, Player>()
                .Create(playerPos));
        }
    }

    public override void SetConfig()
    {
        _config.MaxRoomSize = 20;
        _config.Size = new Vector(80, 30);
        _config.Configure();
    }

    private Vector GetPosition()
    {
        Vector position = Vector.zero;
        bool isOccupied = true;
        do
        {
            Vector randV = Vector.GetRandom().Abs();
            position.X =  randV.X *_rand.Next(0, _tileMap.Size.X);
            position.Y =  randV.Y *_rand.Next(0, _tileMap.Size.Y);
            if (!_tileMap.IsFree(position)) continue;

            var entities = Content.GameEntities ?? Enumerable.Empty<GameEntity>();
            var units = Content.Units ?? Enumerable.Empty<Unit>();
            var items = Content.Items ?? Enumerable.Empty<GameEntity>();

            isOccupied =
                entities.Any(e => e.Position == position) ||
                units.Any(u => u.Position == position) ||
                items.Any(i => i.Position == position);



        } while (isOccupied);

        return position;
    }
}