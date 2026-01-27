namespace CmdWalker;

public abstract class Construction(Vector position) : GameEntity(position), IMovable
{
    private event Action? _moveUp;
    private event Action? _moveDown;
    private event Action? _moveLeft;
    private event Action? _moveRight;
    private event Action? _confirm;
    private event Action? _cancel;

    public abstract void EnterPlacementMode();
    public abstract void ExitPlacementMode();
    
    protected abstract void OnConfirmPlacement();
    protected abstract void OnCancelPlacement();

    protected virtual void BaseEnterPlacementMode()
    {
        _moveUp = () => Move(Vector.Down);
        _moveDown = () => Move(Vector.Up);
        _moveLeft = () => Move(Vector.Left);
        _moveRight = () => Move(Vector.Right);
        _confirm = OnConfirmPlacement;
        _cancel = OnCancelPlacement;
        
        BindMovementInput();
    }

    protected virtual void BaseExitPlacementMode()
    {
        UnbindMovementInput();
    }

    protected void BindMovementInput()
    {
        Input.Bind(ConsoleKey.W, _moveUp!);
        Input.Bind(ConsoleKey.S, _moveDown!);
        Input.Bind(ConsoleKey.A, _moveLeft!);
        Input.Bind(ConsoleKey.D, _moveRight!);
        Input.Bind(ConsoleKey.Enter, _confirm!);
        Input.Bind(ConsoleKey.C, _cancel!);
    }

    protected void UnbindMovementInput()
    {
        if (_moveUp != null) Input.Unbind(ConsoleKey.W, _moveUp);
        if (_moveDown != null) Input.Unbind(ConsoleKey.S, _moveDown);
        if (_moveLeft != null) Input.Unbind(ConsoleKey.A, _moveLeft);
        if (_moveRight != null) Input.Unbind(ConsoleKey.D, _moveRight);
        if (_confirm != null) Input.Unbind(ConsoleKey.Enter, _confirm);
        if (_cancel != null) Input.Unbind(ConsoleKey.C, _cancel);
    }

    public bool CanMoveDir(Vector dir) => Collider.CanMoveTo(dir);

    public void ClearPreviousPosition(char defaultChar = '\0')
    {
        Vector[] positions = Collider.GetPositions();
        char[] backgroundCells = new char[positions.Length];
        
        for (int i = 0; i < positions.Length; i++)
        {
            backgroundCells[i] = defaultChar == 0 
                ? _map.GetCell(positions[i], true) 
                : defaultChar;
        }
        
        _map.SetCells(positions, backgroundCells);
    }

    public void Move(Vector direction)
    {
        if (direction == Vector.Zero || !CanMoveDir(direction))
            return;

        ClearPreviousPosition();
        Transform.Position += direction;
    }
}