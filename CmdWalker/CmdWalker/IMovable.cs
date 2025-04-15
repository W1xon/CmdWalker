namespace CmdWalker
{
    internal interface IMovable
    {
        public bool CanMoveDir (Vector dir);
        public void ClearPreviousPosition (char defaultChar = '\0');
        public void Move(Vector direction);
    }
}
