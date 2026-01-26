namespace CmdWalker;

internal class TableCreator : IEntityCreator
{
    public GameEntity Create(Vector pos)
    {
        return new Table(pos);
    }
}