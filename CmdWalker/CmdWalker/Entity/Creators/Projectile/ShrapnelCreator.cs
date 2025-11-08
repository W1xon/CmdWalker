namespace CmdWalker;

internal class ShrapnelCreator : ProjectileCreator
{
    public override void Set(GameEntity parent, Vector direction)
    {
        _parent = parent;
        _dir = direction;
        _spawnPosition = _parent.Transform.Position;
    }
    public override ICollectable Create( )
    {
        return new Shrapnel(Vector.zero, ItemState.InInventory);
    }
    public override GameEntity CreateOnMap(Vector pos)
    {
        return new Shrapnel(pos, ItemState.OnMap);
    }

    public override GameEntity CreateActive()
    {  
        return new Shrapnel(_spawnPosition, ItemState.Active, _dir, _parent);
    }
}