namespace CmdWalker
{
    internal class Skeleton : Unit
    {
        public PointMover PointMover { get; set; }
        private Vector _dir;
        private int _tickCounter = 0;
        private int _ticksBeforeMove = 10;
        private int _damage = 100;
        public Skeleton(Vector position) : base(position)
        {
            _dir = Vector.Down;
            _health = new Health(100);
            
            Visual = new Glyph(RenderPalette.GetString(TileType.Skeleton), ConsoleColor.DarkBlue);
            PointMover = new PointMover(this);
            Collider.Excludes.Add(typeof(Player));
        }

        public override void Update()
        {

            _tickCounter++;
            if (_tickCounter < _ticksBeforeMove)
            {
                _map.SetCells(Transform.Position, Visual);
                return;
            }
            Move(_dir);
            _tickCounter = 0;
        }
        public override void Move(Vector direction)
        {
            _dir = PointMover.GetDirection(_map);
            if (!CanMoveDir(direction))
                return;
            
            ClearPreviousPosition();
            Transform.Position += direction;
            _map.SetCells(Transform.Position, Visual);
        }
        public override bool CanMoveDir(Vector dir)
        {
            Vector newPos = Transform.Position + dir;
            bool canMove =  Collider.CanMoveTo(dir);
            
            if(TryAttack(newPos)) canMove = false;
            
            return canMove;
        }
        public override void Destroy()
        {
            Debug.AddKill();
            ClearPreviousPosition();
            _map.EntityManager.DeleteEntity(this);
        }

        private bool TryAttack(Vector position)
        {
            foreach (var entity in _map.EntityManager.Entities.Where(e => e.GetType() == typeof(Player) ))
            {
                if (entity.IsIntersection(Collider) && entity is IDamageable damageable && damageable != this)
                {
                    damageable.TakeDamage(_damage);
                    return true;
                }
            }

            return false;
        }
    }
}
