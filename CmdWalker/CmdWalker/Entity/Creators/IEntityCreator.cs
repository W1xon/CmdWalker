namespace CmdWalker
{
    internal interface IEntityCreator: ICreator
    {
        public GameEntity Create(Vector pos);
    }
}
