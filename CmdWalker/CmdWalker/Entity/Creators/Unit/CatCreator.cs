namespace CmdWalker;

internal class CatCreator : IEntityCreator
{
    public GameEntity Create(Vector pos)
    {
        return new Cat(pos);
    }
}