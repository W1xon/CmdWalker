namespace CmdWalker;

internal class Gun : Weapon
{
    private Dictionary<int, Type> _collectableCreators;
    private ProjectileCreator _creator;
    public Gun(Vector position, GameEntity parent, ItemState state) : base(position, parent, state)
    {
        _collectableCreators = new Dictionary<int, Type>()
        {
            {100, typeof(Bullet)},
            {101, typeof(BounceBullet)},
        };
        Visual = new Glyph("\u2566", ConsoleColor.Blue);
    }
    public override string GetName() => "Gun";

    public override IVisual GetVisual() => Visual;

    public override int GetId() => 200;
    
    public override bool IsStackable() => false;
    public override void Execute()
    {
        throw new NotImplementedException();
    }
    public override ItemState GetState() => _state;
    public override void Update()
    {
        if(_state == ItemState.OnMap)
            UpdateOnMap();
    }
    private void UpdateOnMap()
    { 
        _map.SetCells(Position, Visual);
        foreach (var entity in _map.Entities)
        {
            if (entity.IsSelf(Position) && entity != this)
            {
                if (entity is not Player player) continue;
                _parent = player;
                player.Inventory.PickUp(this);
                _map.DeleteEntity(this);
                _state = ItemState.InInventory;
            }
        }
    }
    public override void Fire(Vector dir, Inventory inventory)
    {
        _creator = GetCreator(inventory);
        if (_creator == null) return;
        _creator.Set(_parent, dir);
        var projectile = _creator.CreateActive();
        GameScene.Map.SpawnEntity(projectile);
        inventory.Drop((ICollectable)projectile);
    }

    private ProjectileCreator GetCreator(Inventory inventory)
    {
        foreach (var id in _collectableCreators)
        {
            if (inventory.Contains(id.Key))
                return CreatorRegistry.GetCreator<ProjectileCreator>(id.Value);
        }
        return null;
    }
}