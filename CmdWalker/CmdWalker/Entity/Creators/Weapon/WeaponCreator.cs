namespace CmdWalker;

internal abstract class WeaponCreator : ICollectableCreator
{
    protected GameEntity _parent;
    protected Vector _dir;
    public abstract ICollectable Create( );

    public abstract GameEntity CreateOnMap(Vector position);

    public abstract GameEntity CreateActive();
    
    public abstract void Set(GameEntity parent, Vector direction);
}