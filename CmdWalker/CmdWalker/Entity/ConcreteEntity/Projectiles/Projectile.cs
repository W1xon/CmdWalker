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
        public void Destroy()
        {
            ClearPreviousPosition();
            _map.EntityManager.DeleteEntity(this);
        }
        public virtual bool CanMoveDir(Vector dir)
        {
            return Collider.CanMoveTo(dir);
        }

        protected bool TryKill(Action? beforeDestruction = null)
        { 
            var target = new Vector((Transform.Position.X + _dir.X), (Transform.Position.Y + _dir.Y));
            var entities = _map.EntityManager.Entities;
            for(int i = 0; i < entities.Count; i++)
            {
                var entity = entities[i];  
                if (!entity.IsSelf(target) || entity is not IDamageable damageable) continue;
                if (damageable == _parent) continue;
                if(beforeDestruction is not null) 
                    beforeDestruction.Invoke();
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
                _player = _map.EntityManager.GetPlayer();
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
