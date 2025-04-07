namespace CmdWalker
{
    internal abstract class Projectile : GameEntity, IMovable
    {
        protected GameEntity _parent;
        protected Health _health;
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
        public virtual bool CanMoveDir(Vector dir)
        {
            return Collider.CanMoveTo(dir, newPos =>
            {
                var cell = _map.GetCell(newPos);
                if (cell == Blocks.GetGlyph(Block.Wall)) return true;
                foreach (var entity in _map.Entities)
                {
                    if (entity.IsSelf(newPos) && entity != this) return true;
                }
                return false; 
            });
        }
    }
}
