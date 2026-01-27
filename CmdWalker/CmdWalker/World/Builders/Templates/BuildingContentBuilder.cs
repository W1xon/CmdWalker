namespace CmdWalker;

public class BuildingContentBuilder(LvlConfig config) : ContentBuilder(config)
{
    private Random _rand  = new Random();

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
        
    }

    public override void AddConstruction()
    {
        Content.Constructions = [];
        foreach (var construction in _config.Constructions)
        {
            construction.EnterPlacementMode();
            construction.Transform.Position = GetPosition(construction.Transform.Size);
        }
        Content.Constructions.AddRange(_config.Constructions);
        Content.Constructions.AddRange(HomeData.Construction);
    }

    public override void AddUnits()
    {
        
        Content.Units = new List<Unit>()
        {
            (Cat)CreatorRegistry.GetCreator<CatCreator, Cat>().Create(Vector.One)
        };
    }

    public override void SetConfig()
    {
        _config.Size = new Vector(40, 20);
        _config.Configure();
    }
    private Vector GetPosition(Vector size)
    {
        Vector position = Vector.Zero;
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