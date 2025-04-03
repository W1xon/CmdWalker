using CmdWalker;

namespace CmdWalker
{
    internal class BounceBullet : Projectile, IDestroyable
    {
        private Vector _dir;
        private int _damage;
        public BounceBullet(Vector position, Vector dir, GameEntity parent) : base(position, parent, "+", ConsoleColor.Red)
        {
            _dir = dir;
            _damage = 100;
            _health = new Health(100);
        }
        public override void Update()
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
                _map.SetCells(GetPositions(), Glyph, ConsoleColor.Red);
                return;
            }
            Position += direction;
            _map.SetCells(GetPositions(), Glyph, ConsoleColor.Red);
        }
        public override bool CanMoveDir(Vector dir)
        {
            Vector[] vectors = GetPositions();
            foreach (Vector v in vectors)
            {
                var target = new Vector((v.X + dir.X), (v.Y + dir.Y));
                var cell = _map.GetCell(target);
                if (cell == Blocks.GetGlyph(Block.Wall) || Blocks.ContainsPartUnit(cell))
                {
                    if(Blocks.ContainsPartUnit(cell))
                        _health.Reset();
                    return false;
                }
            }
            return true;
        }
    }
}
