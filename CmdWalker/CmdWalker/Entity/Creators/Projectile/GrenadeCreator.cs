namespace CmdWalker;

internal class GrenadeCreator : ProjectileCreator
{
    public override void Set(GameEntity parent, Vector direction)
    {
        _parent = parent;
        _dir = direction;
        _spawnPosition = _parent.Collider.GetRelativePosition(_dir);
    }
    public override ICollectable Create( )
    {
        return new Grenade(Vector.zero, ItemState.InInventory);
    }
    public override GameEntity CreateOnMap(Vector pos)
    {
        return new Grenade(pos, ItemState.OnMap);
    }

    public override GameEntity CreateActive()
    {  
        return new Grenade(_spawnPosition, ItemState.Active, _dir, _parent);
    }
}