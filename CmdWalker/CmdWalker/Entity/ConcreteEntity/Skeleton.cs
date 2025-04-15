namespace CmdWalker
{
    internal class Skeleton : Unit
    {
        private Vector _dir;
        private Random _rand = new Random();
        private int _step = 0;
        private int _stepMax = 10;
        private int _damage = 100;
        private SearchPath _search;
        public Skeleton(Vector position) : base(position)
        {
            _dir = Vector.down;
            _health = new Health(100);
            
            Glyph = new Glyph(GlyphRegistry.GetString(Entity.Skeleton), ConsoleColor.DarkBlue);
        }

        public override void Update()
        {
            if (_search == null) _search = new SearchPath(_map, Glyph.Size);

            _step++;
            if (_step >= _stepMax)
            {
                CalculateDirection();
                Move(_dir);
                _step = 0;
            }
        }
        public override void Move(Vector direction)
        {
            ClearPreviousPosition();
            if (!CanMoveDir(direction))
            {
                ChangeDirection();
                _map.SetCells(Collider.GetPositions(), Glyph);
                return;
            }
            Position += direction;
            _map.SetCells(Collider.GetPositions(), Glyph);
        }
        public override bool CanMoveDir(Vector dir)
        {
            Vector newPos = Position + dir;
            bool canMove =  Collider.CanMoveTo(dir);
            
            if(TryAttack(newPos)) canMove = false;
            
            return canMove;
        }
        public override void Destroy()
        {
            ConsoleDebugView.AddKill();
            ClearPreviousPosition();
            _map.DeleteEntity(this);
        }
        private void CalculateDirection()
        {
            var player = _map.GetEntity<Player>().First();
            var goal = player != null ? player.Position : Vector.zero;
            _dir = _search.GetNextPosition(Position, goal) - Position;
        }
        private void ChangeDirection()
        {
            switch (_rand.Next(0, 4))
            {
                case 0:
                    _dir = Vector.up;
                    break;
                case 1:
                    _dir = Vector.down;
                    break;
                case 2:
                    _dir = Vector.left;
                    break;
                case 3:
                    _dir = Vector.right;
                    break;

            }
        }

        private bool TryAttack(Vector position)
        {
            foreach (var entity in _map.Entities)
            {
                if (entity.IsSelf(position) && entity is IDamageable damageable && damageable != this)
                {
                    damageable.TakeDamage(_damage);
                    return true;
                }
            }

            return false;
        }
    }
}
