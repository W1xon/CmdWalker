namespace CmdWalker
{
    internal abstract class Unit : GameEntity, IMovable, IDamageable, IDestroyable
    {
        protected Health _health;
        public Unit(Vector position) : base(position) { }
        public abstract void Destroy();
        public abstract void Move(Vector direction);
        public abstract bool CanMoveDir(Vector dir);
        public void ClearPreviousPosition()
        {
            Vector[] positions = Collider.GetPositions();
            char[] backgroundCells = new char[positions.Length];
            for (int i = 0; i < positions.Length; i++)
            {
                backgroundCells[i] = _map.GetCell(positions[i], true);
            }
            _map.SetCells(positions, new string(backgroundCells));
        }

        public void TakeDamage(int damage)
        {
            _health.TryTakeDamage(damage);
            if (_health.IsZero) Destroy();
        }
    }
}
