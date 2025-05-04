namespace CmdWalker
{
    internal abstract class Projectile : GameEntity, IMovable,ICollectable
    {
        protected GameEntity _parent;
        protected Health _health;
        protected Vector _dir;
        protected int _damage;
        protected ItemState _state;
        public Projectile(Vector position, GameEntity parent, ItemState state, Vector dir ) : base(position)
        {
            _parent = parent;
            _dir = dir;
            _state = state;
        }
        public Projectile(Vector position) : base(position){}

        public abstract string GetName();
        public abstract IVisual GetVisual();
        public abstract int GetId();
        public abstract ItemState GetState();
        public abstract bool IsStackable();
        public abstract void Execute();
        public abstract void Move(Vector direction);
        public void ClearPreviousPosition(char defaultChar = '\0')
        {
            Vector[] positions = Collider.GetPositions();
            char[] backgroundCells = new char[positions.Length];
            for (int i = 0; i < positions.Length; i++)
            {
                backgroundCells[i] = defaultChar == 0 ?  _map.GetCell(positions[i], true) : defaultChar;
            }
            _map.SetCells(Position, new string(backgroundCells));
        }
        public void Destroy()
        {
            ClearPreviousPosition();
            _map.DeleteEntity(this);
        }
        public virtual bool CanMoveDir(Vector dir)
        {
            return Collider.CanMoveTo(dir);
        }
        public bool TryKill()
        { 
            var target = new Vector((Position.X + _dir.X), (Position.Y + _dir.Y));
            foreach (var entity in _map.Entities)
            {
                if (entity.IsSelf(target) && entity is IDamageable damageable)
                {
                    if (damageable != _parent)
                    {
                        Destroy();
                        ClearPreviousPosition();
                        damageable.TakeDamage(_damage);
                        return true;
                    }
                }
            }
            return false;
        }
        protected virtual void UpdateOnMap()
        { 
            _map.SetCells(Position, Visual);
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
        protected virtual void UpdateActive() 
        {
            Move(_dir);
        }
    }
}
