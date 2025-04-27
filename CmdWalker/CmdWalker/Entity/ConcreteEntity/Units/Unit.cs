namespace CmdWalker
{
    internal abstract class Unit : GameEntity, IMovable, IDamageable, IDestroyable
    {
        protected Health _health;
        public Unit(Vector position) : base(position) { }
        public abstract void Destroy();
        public abstract void Move(Vector direction);
        public abstract bool CanMoveDir(Vector dir);
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
        public void TakeDamage(int damage)
        {
            _health.TryTakeDamage(damage);
            if (_health.IsZero) Destroy();
        }
    }
}
