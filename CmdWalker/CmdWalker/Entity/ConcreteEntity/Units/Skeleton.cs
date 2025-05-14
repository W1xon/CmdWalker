namespace CmdWalker
{
    internal class Skeleton : Unit
    {
        private Vector _dir;
        private Random _rand = new Random();
        private int _tickCounter = 0;
        private int _ticksBeforeMove = 10;
        private int _damage = 100;
        private SearchPath _pathToTarget;
        public Skeleton(Vector position) : base(position)
        {
            _dir = Vector.down;
            _health = new Health(100);
            
            Visual = new Glyph(RenderPalette.GetString(TileType.Skeleton), ConsoleColor.DarkBlue);
            _pathToTarget = new SearchPath(Visual.Size);
            Collider.Excludes.Add(typeof(Player));
        }

        public override void Update()
        {

            _tickCounter++;
            if (_tickCounter < _ticksBeforeMove) return;
            Move(_dir);
            _tickCounter = 0;
        }
        public override void Move(Vector direction)
        {
            CalculateDirection();
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
            _map.DeleteEntity(this);
        }
        private void CalculateDirection()
        {
            var player = _map.GetEntity<Player>().First();
            var goal = player != null ? player.Transform.Position : Vector.zero;
            int maxIteration = (int)(_map.Size.X * _map.Size.Y * 0.25f);
            _dir = _pathToTarget.GetNextPosition(_map.Plane, Transform.Position, goal, maxIteration) - Transform.Position;
        }
        private bool TryAttack(Vector position)
        {
            foreach (var entity in _map.Entities.Where(e => e.GetType() == typeof(Player) ))
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
