namespace CmdWalker
{
    internal class SkeletonCreator : IEntityCreator
    {
        public  GameEntity Create(Vector pos)
        {
            return new Skeleton(pos);
        }
    }
}
