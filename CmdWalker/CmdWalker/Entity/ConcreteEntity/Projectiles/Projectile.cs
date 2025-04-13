namespace CmdWalker
{
    internal abstract class Projectile : GameEntity, IMovable
    {
        protected GameEntity _parent;
        protected Health _health;
        protected Vector _dir;
        protected int _damage;
        public Projectile(Vector position, GameEntity parent) : base(position)
        {
            _parent = parent;
        }
        public Projectile(Vector position) : base(position){}

        public abstract void Move(Vector direction);
        public void ClearPreviousPosition()
        {
            Vector[] positions = Collider.GetPositions();
            char[] backgroundCells = new char[positions.Length];
            for (int i = 0; i < positions.Length; i++)
            {
                backgroundCells[i] = '.' /*_map.GetCell(positions[i], true)*/;
            }
            _map.SetCells(positions, new string(backgroundCells));
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
    }
}
