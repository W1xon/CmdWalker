namespace CmdWalker
{
    internal abstract class Projectile : GameEntity, IMovable
    {
        protected GameEntity _parent;
        protected Health _health;
        public Projectile(Vector position, GameEntity parent, string glyph, ConsoleColor color) : base(position,glyph, color)
        {
            _parent = parent;
        }
        public Projectile(Vector position, string glyph, ConsoleColor color) : base(position,glyph, color){}

        public abstract void Move(Vector direction);
        public void ClearPreviousPosition()
        {
            Vector[] positions = GetPositions();
            char[] backgroundCells = new char[positions.Length];
            for (int i = 0; i < positions.Length; i++)
            {
                backgroundCells[i] = '.' /*_map.GetCell(positions[i], true)*/;
            }
            _map.SetCells(positions, new string(backgroundCells));
        }
        public virtual bool CanMoveDir(Vector dir)
        {
            Vector[] vectors = GetPositions();
            foreach (Vector v in vectors)
            {
                var target = new Vector((v.X + dir.X), (v.Y + dir.Y));
                var cell = _map.GetCell(target);
                if ( cell == Blocks.GetGlyph(Block.Wall) || Blocks.ContainsPartUnit(cell))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
