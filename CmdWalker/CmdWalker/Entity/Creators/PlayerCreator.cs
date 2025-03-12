namespace CmdWalker
{
    internal class PlayerCreator : EntityCreator
    {
        public override GameEntity Create(Vector pos)
        {
            return new Player(pos);
        }
    }
}
