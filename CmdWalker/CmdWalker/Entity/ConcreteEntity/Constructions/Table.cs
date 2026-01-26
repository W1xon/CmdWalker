namespace CmdWalker;

public class Table : Construction
{
    private event Action _build;
    private event Action _close;
    private static int number = 0;
    // Movement
    private event Action _moveUp;
    private event Action _moveDown;
    private event Action _moveLeft;
    private event Action _moveRight;
    public Table(Vector position) : base(position)
    {
        Visual = new Sprite(
            RenderPalette.GetSprite(TileType.Table),
            ConsoleColor.Yellow
        );
        Layer = 1;
    }

    public override void ChangeToBuild()
    {
        Visual.Color = ConsoleColor.DarkYellow;
        Debug.Info = "";
        
        // Movement actions
        _moveUp = () => Move(Vector.Down);
        _moveDown = () => Move(Vector.Up);
        _moveLeft = () => Move(Vector.Left);
        _moveRight = () => Move(Vector.Right);
        _build = Build;
        _close = Close;
        BindInput();
        number++;
        Debug.Info += number;
    }

    public override void ChangeToStay()
    {
        Visual.Color = ConsoleColor.Yellow;
        Debug.Info = "";
        UnbindInput();
        number--;
        Debug.Info += number;
        SceneManager.SwitchTo(new SafeZoneScene(), LvlDifficult.Easy);
    }

    public override void Build()
    {
        HomeData.Construction.Add(this);
        HomeShope.Construction.Remove(this);
        ChangeToStay();
    }

    public override void Close()
    {
        ChangeToStay();
    }

    public override void Update()
    {
        Render();
    }

    private void Render()
    {
        _map.SetCells(Transform.Position, Visual);
    }

    private void BindInput()
    {
        // Movement (WASD)
        Input.Bind(ConsoleKey.W, _moveUp);
        Input.Bind(ConsoleKey.S, _moveDown);
        Input.Bind(ConsoleKey.A, _moveLeft);
        Input.Bind(ConsoleKey.D, _moveRight);
        Input.Bind(ConsoleKey.Enter, _build);
        Input.Bind(ConsoleKey.C, _close);
    }

    private void UnbindInput()
    {
        Input.Unbind(ConsoleKey.W, _moveUp);
        Input.Unbind(ConsoleKey.S, _moveDown);
        Input.Unbind(ConsoleKey.A, _moveLeft);
        Input.Unbind(ConsoleKey.D, _moveRight);
        Input.Unbind(ConsoleKey.Enter, _build);
        Input.Unbind(ConsoleKey.C, _close);

    }
}