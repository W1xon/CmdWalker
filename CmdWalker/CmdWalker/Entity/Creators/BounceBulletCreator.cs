namespace CmdWalker
{
    internal class BounceBulletCreator : EntityCreator
    {
        private Vector _dir;
        private GameEntity _parent;
        public BounceBulletCreator(Vector direction, GameEntity parent)
        {
            _dir = direction;
            _parent = parent;
        }
        public  GameEntity Create(Vector pos)
        {
            return new BounceBullet(pos, _dir, _parent);
        }
    }
}
