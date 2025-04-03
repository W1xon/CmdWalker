namespace CmdWalker;

internal interface CollectableCreator
{
    public ICollectable Create();
    public GameEntity CreateOnMap(Vector position);
    public GameEntity CreateActive();
}