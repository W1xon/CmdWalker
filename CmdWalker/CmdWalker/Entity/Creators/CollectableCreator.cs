namespace CmdWalker;

internal interface ICollectableCreator : ICreator
{
    public ICollectable Create();
    public GameEntity CreateOnMap(Vector position);
    public GameEntity CreateActive();
}