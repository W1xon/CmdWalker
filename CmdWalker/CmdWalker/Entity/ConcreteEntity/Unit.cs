namespace CmdWalker
{
    internal abstract class Unit : GameEntity, IMovable, IDamageable
    {
        protected Health _health;
        public Unit(Vector position, string glyph, ConsoleColor color) : base(position, glyph, color) { }

        public abstract void Move(Vector direction);
        public  virtual bool CanMoveDir(Vector dir)
        {
            Vector[] vectors = GetPositions();
            foreach (Vector v in vectors)
            {
                if (_map.GetCell(new Vector((v.X + dir.X), (v.Y + dir.Y))) == Blocks.GetGlyph(Block.Wall))
                {
                    return false;
                }
            }
            return true;
        }
        public void ClearPreviousPosition()
        {
            Vector[] positions = GetPositions();
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
