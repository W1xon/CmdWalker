namespace CmdWalker
{
    internal class BounceBulletCreator : ProjectileCreator
    {
        public override void Set(GameEntity parent, Vector direction)
        {
            _parent = parent;
            _dir = direction;
            _spawnPosition = _parent.Collider.GetRelativePosition(_dir);
        }
        public override ICollectable Create( )
        {
            return new BounceBullet(Vector.zero, ItemState.InInventory);
        }
        public override GameEntity CreateOnMap(Vector pos)
        {
            return new BounceBullet(pos, ItemState.OnMap);
        }

        public override GameEntity CreateActive()
        {  
            return new BounceBullet(_spawnPosition, ItemState.Active, _dir, _parent);
        }

    }
}
