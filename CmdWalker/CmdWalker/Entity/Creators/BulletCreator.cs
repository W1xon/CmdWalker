namespace CmdWalker
{
    internal class BulletCreator :  CollectableCreator
    {
        private GameEntity _parent;
        private Vector _dir;
        public void Set(GameEntity parent, Vector direction)
        {
            _parent = parent;
            _dir = direction;
        }
        public ICollectable Create()
        {
            return new Bullet(Vector.zero, ItemState.InInventory);
        }
        public GameEntity CreateOnMap(Vector pos)
        {
            return new Bullet(pos, ItemState.OnMap);
        }

        public GameEntity CreateActive()
        {  
            return new Bullet(_parent.Position, ItemState.Active, _dir, _parent);
        }

    }
}
