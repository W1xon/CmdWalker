namespace CmdWalker
{
    internal class BulletCreator : EntityCreator
    {
        private Vector _dir;
        private GameEntity _parent;
        public BulletCreator(Vector direction, GameEntity parent)
        {
            _dir = direction;
            _parent = parent;
        }
        public override GameEntity Create(Vector pos)
        {
            return new Bullet(pos, _dir, _parent);
        }
    }
}
