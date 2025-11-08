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
        creator.Set(GameScene.Map.EntityManager.GetEntity<Player>().First(),
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
        List<Vector> rotatedDirection = GenerateDiscreteCircle(_rand.Next(2,5));
        var creator = CreatorRegistry.GetCreator<ShrapnelCreator, Shrapnel>();
        for (int i = 0; i < rotatedDirection.Count; i ++)
        {
            var dir = rotatedDirection[i];
            creator.Set(this, dir);
            GameScene.Map.EntityManager.SpawnEntity(creator.CreateActive());
        }
    }

    private List<Vector> GenerateDiscreteCircle(int radius)
    {
        List<Vector> result = new List<Vector>(radius*radius);
    
        for (int y = -radius; y <= radius; y++)
        {
            for (int x = -radius; x <= radius; x++)
            {
                if (x == 0 && y == 0)
                    continue; 

                double distance = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
                
                double offset = 0.2;
                if (distance <= radius + (radius * 0.2))
                    result.Add(new Vector(x, y));
            }
        }

        return result;
    }


}