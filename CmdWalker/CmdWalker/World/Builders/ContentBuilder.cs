namespace CmdWalker;

internal enum LvlTypeGeneration
{
    BSP,
    SafeZone,
}

public abstract class ContentBuilder(LvlConfig config)
{
    
    protected MapContent Content = new();
    protected LvlConfig _config = config;
    protected TileMap _tileMap;
    
    protected Random _rand = new Random();
    public abstract void AddCarcassBuilder();
    public abstract  void AddEntities();
    public abstract void AddItems();
    public abstract void AddConstruction();
    public abstract void AddUnits();
    public abstract void SetConfig();
    public void SetTileMap(TileMap tileMap) => _tileMap  = tileMap;
    public MapContent GetTemplate() => Content;
    
    
    protected Vector GetFreePosition(Vector size)
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