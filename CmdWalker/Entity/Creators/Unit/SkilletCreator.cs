namespace CmdWalker
{
    internal class SkilletCreator : IEntityCreator
    {
        public  GameEntity Create(Vector pos)
        {
            return new Skeleton(pos);
        }
    }
}
