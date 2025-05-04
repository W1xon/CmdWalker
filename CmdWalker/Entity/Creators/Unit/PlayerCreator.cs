namespace CmdWalker
{
    internal class PlayerCreator : IEntityCreator
    {
        public GameEntity Create(Vector pos)
        {
            return new Player(pos);
        }
    }
}
