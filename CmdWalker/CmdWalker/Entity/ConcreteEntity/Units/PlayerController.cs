namespace CmdWalker;

public class PlayerController : IDisposable
{
    private Player _player;
    
    // Movement
    private readonly Action _moveUp;
    private readonly Action _moveDown;
    private readonly Action _moveLeft;
    private readonly Action _moveRight;

    // Use item
    private readonly Action _useUp;
    private readonly Action _useDown;
    private readonly Action _useLeft;
    private readonly Action _useRight;

    // Equip 1–9
    private readonly Action[] _equipActions = new Action[9];
    public PlayerController(Player player)
    {
        _player = player;
        
        // Movement actions
        _moveUp    = () => _player.Move(Vector.Down);
        _moveDown  = () => _player.Move(Vector.Up);
        _moveLeft  = () => _player.Move(Vector.Left);
        _moveRight = () => _player.Move(Vector.Right);

        // Use actions
        _useUp    = () => Use(Vector.Up);
        _useDown  = () => Use(Vector.Down);
        _useLeft  = () => Use(Vector.Left);
        _useRight = () => Use(Vector.Right);

        BindInput();
    }
    

    private void BindInput()
    {
        // Movement (WASD)
        Input.Bind(ConsoleKey.W, _moveUp);
        Input.Bind(ConsoleKey.S, _moveDown);
        Input.Bind(ConsoleKey.A, _moveLeft);
        Input.Bind(ConsoleKey.D, _moveRight);

        // Use item (IJKL)
        Input.Bind(ConsoleKey.I, _useDown);
        Input.Bind(ConsoleKey.K, _useUp);
        Input.Bind(ConsoleKey.J, _useLeft);
        Input.Bind(ConsoleKey.L, _useRight);

        // Equip slots 1–9
        for (int i = 0; i < 9; i++)
        {
            int slot = i;

            _equipActions[i] = () =>
                _player.Inventory.TryEquip(
                    slot,
                    collectable =>
                        collectable.GetVisual().Size.X
                        <= _player.Collider.GetDistance(Vector.Right)
                );

            Input.Bind(ConsoleKey.D1 + i, _equipActions[i]);
        }
    }

    private void UnbindInput()
    {
        Input.Unbind(ConsoleKey.W, _moveUp);
        Input.Unbind(ConsoleKey.S, _moveDown);
        Input.Unbind(ConsoleKey.A, _moveLeft);
        Input.Unbind(ConsoleKey.D, _moveRight);

        Input.Unbind(ConsoleKey.I, _useDown);
        Input.Unbind(ConsoleKey.K, _useUp);
        Input.Unbind(ConsoleKey.J, _useLeft);
        Input.Unbind(ConsoleKey.L, _useRight);

        for (int i = 0; i < 9; i++)
        {
            Input.Unbind(ConsoleKey.D1 + i, _equipActions[i]);
        }
    }

    private void Use(Vector dir)
    {
        _player.Inventory.TryUse(
            dir,
            collectable =>
                collectable.GetVisual().Size.X
                <= _player.Collider.GetDistance(dir)
        );
    }

    public void Dispose()
    {
        UnbindInput();
    }
}