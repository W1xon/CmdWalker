namespace CmdWalker
{
    internal class Skillet : Unit
    {
        private Vector _dir;
        private Random _rand = new Random();
        private int _step = 0;
        private int _stepMax = 5;
        private int _damage = 100;
        public Skillet(Vector position) : base(position, Blocks.GetUnitGlyph(Units.Skillet), ConsoleColor.DarkBlue)
        {
            _dir = Vector.down;
            _health = new Health(100);
        }

        public override void Update()
        {
            _step++;
            if (_step >= _stepMax)
            {
                if(_rand.Next(0, 10) < 1) ChangeDirection();
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
                _map.SetCells(GetPositions(), Glyph, BodyColor);
                return;
            }
            Position += direction;
            _map.SetCells(GetPositions(), Glyph, BodyColor);
        }
        public override bool CanMoveDir(Vector dir)
        {
            Vector[] positions = GetPositions();
            bool canMove = true;

            foreach (Vector currentPos in positions)
            {
                Vector newPos = currentPos + dir;
                char cell = _map.GetCell(newPos);

                if (cell != Blocks.GetGlyph(Block.Floor))
                {
                    canMove = false;
                    continue;
                }

                foreach (var entity in _map.Entities)
                {
                    if (entity.IsEntity(newPos) && entity is IDamageable damageable && damageable != this)
                    {
                        damageable.TakeDamage(_damage);
                        return true;
                    }
                }
            }
            return canMove;
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
        public override void Destroy()
        {
            ConsoleDebugView.AddKill();
            ClearPreviousPosition();
            _map.DeleteEntity(this);
        }
    }
}
