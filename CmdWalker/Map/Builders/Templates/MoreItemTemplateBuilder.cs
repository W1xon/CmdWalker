namespace CmdWalker;

internal class MoreItemTemplateBuilder : TemplateBuilder
{
    public override void AddEntities()
    {
        _template.GameEntities = new List<GameEntity>()
        {
            CreatorRegistry.GetCreator<PortalCreator, Portal>().Create(new Vector(0, 10), true),
            CreatorRegistry.GetCreator<PortalCreator, Portal>().Create(new Vector(10, 0)),
        };
    }

    public override void AddItems()
    {
        _template.Items = new List<GameEntity>()
        {
            CreatorRegistry.GetCreator<BulletCreator, Bullet>().CreateOnMap(new Vector(10, 10)),
            CreatorRegistry.GetCreator<BulletCreator, Bullet>().CreateOnMap(new Vector(20, 19)),
            CreatorRegistry.GetCreator<BulletCreator, Bullet>().CreateOnMap(new Vector(39, 1)),
            CreatorRegistry.GetCreator<BulletCreator, Bullet>().CreateOnMap(new Vector(40, 1)),
            CreatorRegistry.GetCreator<BounceBulletCreator, BounceBullet>().CreateOnMap(new Vector(41, 1)),
            CreatorRegistry.GetCreator<BounceBulletCreator, BounceBullet>().CreateOnMap(new Vector(70, 6)),
            CreatorRegistry.GetCreator<GunCreator, Gun>().CreateOnMap(new Vector(4, 6)),
        };
    }

    public override void AddUnits()
    {
        _template.Units = new List<Unit>()
        {
            (Skeleton)(CreatorRegistry.GetCreator<SkilletCreator, Skeleton>().Create(new Vector(60, 5))),
            (Skeleton)(CreatorRegistry.GetCreator<SkilletCreator, Skeleton>().Create(new Vector(70, 15))),
        };
        var player = GameScene.Map.GetEntity<Player>().FirstOrDefault();
        var playerPos = new Vector(10, 3);
        if (player != null)
        {
            player.Position = playerPos;
            _template.GameEntities.Add(player);
        }
        else
        {
            _template.GameEntities.Add((Player)CreatorRegistry.GetCreator<PlayerCreator, Player>()
                .Create(playerPos));
        }
    }

    public override void SetConfig()
    {
        _template.MaxRoomSize = 20;
        _template.Size = new Vector(80, 30);
    }
}