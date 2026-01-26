namespace CmdWalker;

public abstract class Construction(Vector position) : GameEntity(position), IMovable
{
    

    public abstract void Build();
    public abstract void Close();

    public abstract void ChangeToBuild();

    public abstract void ChangeToStay();
    public bool CanMoveDir(Vector dir) => Collider.CanMoveTo(dir);

    public void ClearPreviousPosition(char defaultChar = '\0')
    {
        Vector[] positions = Collider.GetPositions();
        char[] backgroundCells = new char[positions.Length];
        for (int i = 0; i < positions.Length; i++)
        {
            backgroundCells[i] = defaultChar == 0 ?  _map.GetCell(positions[i], true) : defaultChar;
        }
        _map.SetCells(positions, backgroundCells);
    }
    public void Move(Vector direction)
    {
        if (direction == Vector.Zero)
            return;

        if (!CanMoveDir(direction))
            return;

        ClearPreviousPosition();
        Transform.Position += direction;
    }
}