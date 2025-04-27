namespace CmdWalker;

internal abstract class ProjectileCreator : ICollectableCreator
{
    protected Vector _spawnPosition;
    protected Vector _dir;
    protected GameEntity _parent;
    public ProjectileCreator(){}
    public abstract ICollectable Create( );

    public abstract GameEntity CreateOnMap(Vector position);

    public abstract GameEntity CreateActive();
    public abstract void Set(GameEntity parent, Vector direction);

    
}