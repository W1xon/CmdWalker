namespace CmdWalker
{
    internal class PlayerCreator : EntityCreator
    {
        public  GameEntity Create(Vector pos)
        {
            return new Player(pos);
        }
    }
}
