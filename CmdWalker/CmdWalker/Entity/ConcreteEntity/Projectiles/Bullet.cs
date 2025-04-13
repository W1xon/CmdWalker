namespace CmdWalker
{
    internal class Bullet : Projectile, IDestroyable, ICollectable
    {
        private Vector _dir;
        private int _damage;
        private ItemState _state;

        public Bullet(Vector position,  ItemState state, Vector dir, GameEntity parent) : base(position, parent)
        {
            _dir = dir;
            _damage = 100;
            _state = state;
            
            Glyph = new Glyph( "*", ConsoleColor.Red);
        }

        public Bullet(Vector position, ItemState state) : base(position)
        {
            _state = state;
            
            Glyph = new Glyph( "*", ConsoleColor.Cyan);
        }
        
        public string GetName() => "Bullet";
        public Glyph GetGlyph() => Glyph;

        public int GetId() => 100;
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

        
        public override void Move(Vector direction)
        {
            ClearPreviousPosition();
            if (!CanMoveDir(direction))
            {
                if(!TryKill())
                    Destroy();
                return;
            }
            Position += direction;
            _map.SetCells(Collider.GetPositions(), Glyph);
        }
    }
}
