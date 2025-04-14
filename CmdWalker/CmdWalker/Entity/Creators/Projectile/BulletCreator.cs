using System.Numerics;

namespace CmdWalker
{
    internal class BulletCreator :  ProjectileCreator
    {
        public override void Set(GameEntity parent, Vector direction)
        {
            _parent = parent;
            _dir = direction;
            CalculateSpawnPosition();
        }
        public override ICollectable Create( )
        {
            return new Bullet(Vector.zero, ItemState.InInventory);
        }
        public override GameEntity CreateOnMap(Vector pos)
        {
            return new Bullet(pos, ItemState.OnMap);
        }

        public override GameEntity CreateActive()
        {  
            return new Bullet(_spawnPosition, ItemState.Active, _dir, _parent);
        }

    }
}
