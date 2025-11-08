namespace CmdWalker;

internal class GunCreator : WeaponCreator
{
    public override ICollectable Create( )
    {
        return new Gun(Vector.zero, _parent, ItemState.InInventory);
    }

    public override GameEntity CreateOnMap(Vector position)
    {
        return new Gun(position, _parent, ItemState.OnMap);
    }

    public override GameEntity CreateActive()
    {
        return new Gun(Vector.zero, _parent, ItemState.Active);
    }

    public override void Set(GameEntity parent, Vector direction)
    {
        
    }
}