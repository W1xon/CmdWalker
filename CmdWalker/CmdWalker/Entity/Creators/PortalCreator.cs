namespace CmdWalker;

internal class PortalCreator : IEntityCreator
{
    public GameEntity Create(Vector pos)
    {
        return new Portal(pos);
    }

    public GameEntity Create(Vector pos, bool isIn)
    {
        return new Portal(pos, isIn);
    }
}