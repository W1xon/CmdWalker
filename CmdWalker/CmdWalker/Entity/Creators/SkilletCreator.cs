namespace CmdWalker
{
    internal class SkilletCreator : EntityCreator
    {
        public  GameEntity Create(Vector pos)
        {
            return new Skillet(pos);
        }
    }
}
