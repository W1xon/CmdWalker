namespace CmdWalker;

internal class DeathScene : GameScene
{
    private readonly Vector _dimensions;
    private int _frame;
    private Action _onReboot;

    public DeathScene(Vector dimensions) => _dimensions = dimensions;

    public override void Enter()
    {
        base.Enter();
        InitCanvas();
        _onReboot = () => SceneManager.SwitchTo(new MenuScene(_dimensions), LvlDifficult.Easy);
        Input.Bind(ConsoleKey.R, _onReboot);
    }

    public override void Update()
    {
        _frame++;
        
        for (int i = 0; i < 5; i++)
        {
            var p = new Vector(Random.Shared.Next(_dimensions.X), Random.Shared.Next(_dimensions.Y));
            _canvas.Write(p, ((char)Random.Shared.Next(33, 126)).ToString(), ConsoleColor.DarkRed);
        }

        string msg = " [ FATAL ERROR: CONNECTION LOST ] ";
        ConsoleColor msgCol = _frame % 10 < 5 ? ConsoleColor.Red : ConsoleColor.DarkRed;
        _canvas.Write(new Vector((_dimensions.X - msg.Length)/2, _dimensions.Y/2), msg, msgCol);
        
        string prompt = "PRESS 'R' TO REBOOT SYSTEM";
        _canvas.Write(new Vector((_dimensions.X - prompt.Length)/2, _dimensions.Y/2 + 3), prompt, ConsoleColor.Gray);

        Render();
    }

    public override void Exit()
    {
        base.Exit();
        Input.Unbind(ConsoleKey.R, _onReboot);
    }

    public override void InitCanvas() => _canvas = new Canvas(_dimensions);
    public override void Enter(LvlDifficult lvl) => Enter();
}