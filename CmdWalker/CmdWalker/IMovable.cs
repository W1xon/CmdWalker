namespace CmdWalker
{
    internal interface IMovable
    {
        public bool CanMoveDir (Vector dir);
        public void ClearPreviousPosition ();
        public void Move(Vector direction);
    }
}
