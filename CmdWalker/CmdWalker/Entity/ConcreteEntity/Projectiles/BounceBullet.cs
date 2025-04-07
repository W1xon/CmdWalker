namespace CmdWalker
{
    internal class BounceBullet : Projectile, IDestroyable, ICollectable
    {
        private Vector _dir;
        private int _damage;
        private ItemState _state;
        public BounceBullet(Vector position,  ItemState state, Vector dir, GameEntity parent) : base(position, parent)
        {
            _dir = dir;
            _damage = 100;
            _state = state;
            _health = new Health(100);
            
            Glyph = new Glyph( "+", ConsoleColor.Red);
        }

        public BounceBullet(Vector position, ItemState state) : base(position)
        {
            _state = state;
            Glyph = new Glyph( "+", ConsoleColor.Cyan);
        }
        public string GetName() => "BounceBullet";
        public Glyph GetGlyph() => Glyph;

        public int GetId() => 101;
        public ItemState GetState() => _state;

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
            _map.SetCells([Position], Glyph);
            foreach (var entity in _map.Entities)
            {
                if (entity.IsSelf(Position) && entity != this)
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
                if (entity.IsSelf(target) && entity is IDamageable damageable)
                {
                    if (damageable != _parent)
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
                if (!_health.TryTakeDamage(30))
                {
                    Destroy();
                    return;
                }
                _dir = Vector.GetRandom(direction, true);
                _map.SetCells(Collider.GetPositions(), Glyph);
                return;
            }
            Position += direction;
            _map.SetCells(Collider.GetPositions(), Glyph);
        }
        public override bool CanMoveDir(Vector dir)
        {
            return Collider.CanMoveTo(dir, newPos =>
            {
                var cell = _map.GetCell(newPos);
                if (cell == Blocks.GetGlyph(Block.Wall)) return true;
                foreach (var entity in _map.Entities)
                {
                    if (entity.IsSelf(newPos) && entity != this)
                    {
                        _health.Reset();
                        return true;
                    }
                }
                return false; 
            });
        }
    }
}
