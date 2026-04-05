namespace CmdWalker;

internal class MenuScene : GameScene
{
    private readonly Vector _dimensions;
    private readonly string[] _buttons = { "INITIALIZE CORE", "TERMINATE" };
    private int _selectedIndex;
    private int _frame;
    private readonly List<Vector> _stars = [];
    private CancellationTokenSource _musicCts;

    
    private Action _onUp, _onDown, _onSelect;

    private readonly string[] _asciiTitle = {
        @"  ____                _  __        __    _ _            ",
        @" / ___|_ __ ___   __| | \ \      / /_ _| | | _____ _ __ ",
        @"| |   | '_ ` _ \ / _` |  \ \ /\ / / _` | | |/ / _ \ '__|",
        @"| |___| | | | | | (_| |   \ V  V / (_| | |   <  __/ |   ",
        @" \____|_| |_| |_|\__,_|    \_/\_/ \__,_|_|_|\_\___|_|   "
    };

    public MenuScene(Vector dimensions)
    {
        _dimensions = dimensions;
        
        for (int i = 0; i < 40; i++)
            _stars.Add(new Vector(Random.Shared.Next(1, dimensions.X - 1), Random.Shared.Next(1, dimensions.Y - 1)));
    }

    public override void Enter()
    {
        base.Enter();
        InitCanvas();
    
        DrawBackground(); 
        DrawBorder();     
        DrawTitle();      
        DrawButtons();    
        Render(); 

        _onUp = () => _selectedIndex = 0;
        _onDown = () => _selectedIndex = 1;
        _onSelect = ActivateSelected;

        Input.Bind(ConsoleKey.UpArrow, _onUp);
        Input.Bind(ConsoleKey.DownArrow, _onDown);
        Input.Bind(ConsoleKey.Enter, _onSelect);

        _musicCts = new CancellationTokenSource();
    }

    public override void Update()
    {
        _frame++;
        UpdateStars();
        
        DrawBackground(); 
        DrawBorder();     
        DrawTitle();      
        DrawButtons();    
        
        Render();
    }

    private void UpdateStars()
    {
        if (_frame % 3 != 0) return; 
        for (int i = 0; i < _stars.Count; i++)
        {
            var s = _stars[i];
            s.Y++;
            if (s.Y >= _dimensions.Y - 1) s.Y = 1;
            _stars[i] = s;
        }
    }

    private void DrawBackground()
    {
        for (int y = 1; y < _dimensions.Y - 1; y++)
            _canvas.Write(new Vector(1, y), new string(' ', _dimensions.X - 2), ConsoleColor.Black);

        foreach (var s in _stars)
            _canvas.Write(s, ".", ConsoleColor.DarkGray);
    }

    private void DrawBorder()
    {
        ConsoleColor borderColor = (_frame / 5) % 2 == 0 ? ConsoleColor.Cyan : ConsoleColor.DarkCyan;
        string horizontal = new('═', _dimensions.X - 2);
        
        _canvas.Write(new Vector(1, 0), horizontal, borderColor);
        _canvas.Write(new Vector(1, _dimensions.Y - 1), horizontal, borderColor);
        
        for (int y = 1; y < _dimensions.Y - 1; y++)
        {
            _canvas.Write(new Vector(0, y), "║", borderColor);
            _canvas.Write(new Vector(_dimensions.X - 1, y), "║", borderColor);
        }
        
        _canvas.Write(new Vector(0, 0), "╔", borderColor);
        _canvas.Write(new Vector(_dimensions.X - 1, 0), "╗", borderColor);
        _canvas.Write(new Vector(0, _dimensions.Y - 1), "╚", borderColor);
        _canvas.Write(new Vector(_dimensions.X - 1, _dimensions.Y - 1), "╝", borderColor);
    }

    private void DrawTitle()
    {
        int startY = _dimensions.Y / 6;
        
        ConsoleColor titleColor = (_frame / 15) % 2 == 0 ? ConsoleColor.White : ConsoleColor.Cyan;

        for (int i = 0; i < _asciiTitle.Length; i++)
        {
            int x = (_dimensions.X - _asciiTitle[i].Length) / 2;
            _canvas.Write(new Vector(x, startY + i), _asciiTitle[i], titleColor);
        }
    }

    private void DrawButtons()
    {
        int btnY = _dimensions.Y / 2 + 3;
        for (int i = 0; i < _buttons.Length; i++)
        {
            bool sel = i == _selectedIndex;
            string text = sel ? $" > [ {_buttons[i]} ] < " : $"     {_buttons[i]}     ";
            ConsoleColor col = sel ? ConsoleColor.Yellow : ConsoleColor.DarkGray;
            _canvas.Write(new Vector((_dimensions.X - text.Length) / 2, btnY + i * 2), text, col);
        }
    }

    public override void Exit()
    {
        base.Exit();
        _musicCts?.Cancel(); 
        Input.Unbind(ConsoleKey.UpArrow, _onUp);
        Input.Unbind(ConsoleKey.DownArrow, _onDown);
        Input.Unbind(ConsoleKey.Enter, _onSelect);
    }

    private void ActivateSelected()
    {
        if (_selectedIndex == 0) SceneManager.SwitchTo(new SafeZoneScene(), LvlDifficult.Easy);
        else Environment.Exit(0);
    }

    public override void InitCanvas() => _canvas = new Canvas(_dimensions);
    public override void Enter(LvlDifficult lvl) => Enter();
}