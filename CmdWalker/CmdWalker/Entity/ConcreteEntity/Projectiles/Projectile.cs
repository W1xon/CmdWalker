namespace CmdWalker
{
    internal abstract class Projectile : GameEntity, IMovable,ICollectable
    {
        protected GameEntity _parent;
        protected Health _health;
        protected Vector _dir;
        protected int _damage;
        protected ItemState _state;
        protected Player _player;
        public Projectile(Vector position, GameEntity parent, ItemState state, Vector dir ) : base(position)
        {
            _parent = parent;
            _dir = dir;
            _state = state;
            Layer = 1;
        }
        public Projectile(Vector position) : base(position){}

        public abstract string GetName();
        public abstract IVisual GetVisual();
        public abstract int GetId();
        public abstract ItemState GetState();
        public abstract bool IsStackable();
        public abstract void Execute();

        public virtual void Launch(Vector direction)
        {
        }

        public abstract void Move(Vector direction);
        public void ClearPreviousPosition(char defaultChar = '\0')
        {
            Vector[] positions = Collider.GetPositions();
            char[] backgroundCells = new char[positions.Length];
            for (int i = 0; i < positions.Length; i++)
            {
                backgroundCells[i] = defaultChar == 0 ?  _map.GetCell(positions[i], true) : defaultChar;
            }
            _map.SetCells(positions, backgroundCells);
        }
        public void Destroy()
        {
            ClearPreviousPosition();
            _map.EntityManager.DeleteEntity(this);
        }
        public virtual bool CanMoveDir(Vector dir)
        {
            return Collider.CanMoveTo(dir);
        }

        protected bool TryKill()
        { 
            var target = new Vector((Transform.Position.X + _dir.X), (Transform.Position.Y + _dir.Y));
            foreach (var entity in _map.EntityManager.Entities)
            {
                if (!entity.IsSelf(target) || entity is not IDamageable damageable) continue;
                if (damageable == _parent) continue;
                
                Destroy();
                ClearPreviousPosition();
                damageable.TakeDamage(_damage);
                return true;
            }
            return false;
        }
        protected void UpdateOnMap()
        {
            _map.SetCells(Transform.Position, Visual);
            if(_player == null)
                _player = _map.EntityManager.GetEntity<Player>().First();
            if (_player.IsSelf(Transform.Position))
            {
                _player.Inventory.PickUp(this);
                _map.EntityManager.DeleteEntity(this);
                _state = ItemState.InInventory;
            }
            
        }
        protected  void UpdateActive() 
        {
            Move(_dir);
        }
    }
}
