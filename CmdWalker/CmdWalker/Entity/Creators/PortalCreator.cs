namespace CmdWalker;

internal class PortalCreator : IEntityCreator
{
    public GameEntity Create(Vector pos)
    {
        return new Portal(pos);
    }
}