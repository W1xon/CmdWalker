namespace CmdWalker
{
    internal class BounceBulletCreator : ProjectileCreator
    {
        public override void Set(GameEntity parent, Vector direction)
        {
            _parent = parent;
            _dir = direction;
        }
        public override ICollectable Create()
        {
            return new BounceBullet(Vector.zero, ItemState.InInventory);
        }
        public override GameEntity CreateOnMap(Vector pos)
        {
            return new BounceBullet(pos, ItemState.OnMap);
        }

        public override GameEntity CreateActive()
        {  
            return new BounceBullet(_parent.Position, ItemState.Active, _dir, _parent);
        }

    }
}
