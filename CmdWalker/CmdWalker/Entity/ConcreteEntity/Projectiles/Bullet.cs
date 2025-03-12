namespace CmdWalker
{
    internal class Bullet : Projectile
    {
        private Vector _dir;
        private int _damage;
        public Bullet(Vector position, Vector dir, GameEntity parent) : base(position, parent, "*", ConsoleColor.Red)
        {
            _dir = dir;
            _damage = 100;
        }
        public override void Update()
        {
            Move(_dir);
        }
        public override void Destroy()
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
