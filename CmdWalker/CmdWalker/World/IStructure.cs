namespace CmdWalker
{
    public enum Direction
    {
        South,
        North,
        West,
        East,
    }

    public interface IStructure
    {
        public void Build(Map map);
        public void Create();
    }
}
