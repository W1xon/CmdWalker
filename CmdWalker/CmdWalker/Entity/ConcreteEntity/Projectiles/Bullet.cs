namespace CmdWalker
{
    internal class Bullet : Projectile, IDestroyable, ICollectable
    {
        private Vector _dir;
        private int _damage;
        private ItemState _state;

        public Bullet(Vector position,  ItemState state, Vector dir = default, GameEntity parent = null) : base(position, parent, "*", ConsoleColor.Red)
        {
            _dir = dir;
            _damage = 100;
            _state = state;
        }

        public Bullet(Vector position, ItemState state) : base(position, "*", ConsoleColor.Red)
        {
            _state = state;
        }
        
        public string GetName() => "Bullet";

        public int GetId() => 1;

        public bool IsStackable() => true;
        public void Execute() => _state = ItemState.Active; 
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
        private void UpdateOnMap()
        { 
            _map.SetCells([Position], Glyph, ConsoleColor.Cyan);
            foreach (var entity in _map.Entities)
            {
                if (entity.IsEntity(Position) && entity != this)
                {
                    if(entity is Player player)
                    {
                        player.Inventory.PickUp(this);
                        _map.DeleteEntity(this);
                        _state = ItemState.InInventory;
                    }
                }
            }
        }
        private void UpdateActive() 
        {
            Move(_dir);
        }
        public void Destroy()
        {
            var target = new Vector((Position.X + _dir.X), (Position.Y + _dir.Y));
            foreach (var entity in _map.Entities)
            {
                if (entity.IsEntity(target) && entity is IDamageable damageable)
                {
                    if(damageable != _parent)
                     damageable.TakeDamage(_damage);
                }
            }
            ClearPreviousPosition();
            _map.DeleteEntity(this);
        }

        public override void Move(Vector direction)
        {
            ClearPreviousPosition();
            if (!CanMoveDir(direction))
            {
                Destroy();
                return;
            }
            Position += direction;
            _map.SetCells(GetPositions(), Glyph, ConsoleColor.Red);
        }
    }
}
