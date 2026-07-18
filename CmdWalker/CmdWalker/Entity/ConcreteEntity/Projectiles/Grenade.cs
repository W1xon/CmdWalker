namespace CmdWalker;

internal class Grenade : Projectile
{
    private int _distance = 25;
    private int _shrapnelsCount;
    private Random _rand = new Random();
    public Grenade(Vector position,  ItemState state, Vector dir, GameEntity parent) : base(position, parent, state, dir)
    {
        _state = state;
        Visual = new Glyph( "@", ConsoleColor.Red);
    }

    public Grenade(Vector position, ItemState state) : base(position)
    {
        _state = state;
            
        Visual = new Glyph( "@", ConsoleColor.Cyan);
    }

    public override string GetName() => "Grenade";

    public override IVisual GetVisual() => Visual;

    public override int GetId() => 200;

    public override ItemState GetState() => _state;

    public override bool IsStackable() => true;

    public override void Execute()
    {
    }

    public override void Launch(Vector direction)
    {
        var creator = CreatorRegistry.GetCreator<GrenadeCreator, Grenade>();
        creator.Set(GameScene.Map.EntityManager.GetPlayer(),
            direction);
        GameScene.Map.EntityManager.SpawnEntity(creator.CreateActive());
    }

    public override void Update()
    {
        switch (_state)
        {
            case ItemState.OnMap:
                UpdateOnMap();
                break;
            case ItemState.Active:
                UpdateActive();
                break;
        }
    }
    public override void Move(Vector direction)
    {
        _distance--;
        if (CanMoveDir(direction) && _distance > 0)
        {    
            ClearPreviousPosition();
            Transform.Position += direction;
            _map.SetCells(Transform.Position, Visual);
            return;
        }
        if(!TryKill() ||  _distance <= 0 )
        {
            Boom();
            Destroy();
        }
    }
    private void Boom()
    {
        int radius = _rand.Next(2, 5);
        int maxSize = (2 * radius + 1) * (2 * radius + 1);
        Span<Vector> rotatedDirection = stackalloc Vector[maxSize];
        int count = FillDiscreteCircle(rotatedDirection, radius);
        var creator = CreatorRegistry.GetCreator<ShrapnelCreator, Shrapnel>();
        for (int i = 0; i < count; i ++)
        {
            var dir = rotatedDirection[i];
            creator.Set(this, dir);
            GameScene.Map.EntityManager.SpawnEntity(creator.CreateActive());
        }
    }

    private int FillDiscreteCircle(Span<Vector> buffer, int radius)
    {
        int i = 0;
        for (int y = -radius; y <= radius; y++)
        {
            for (int x = -radius; x <= radius; x++)
            {
                if (x == 0 && y == 0)
                    continue; 

                double distance = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
                double offset = 0.2;
                if (distance <= radius + (radius * 0.2))
                    buffer[i++] = new Vector(x, y);
            }
        }

        return i;
    }
}