namespace CmdWalker
{
    internal class SkilletCreator : EntityCreator
    {
        public override GameEntity Create(Vector pos)
        {
            return new Skillet(pos);
        }
    }
}
