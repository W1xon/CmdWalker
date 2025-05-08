namespace CmdWalker;

internal class DeathMenu : IScene
{
    public bool IsActive { get; set; }
    private readonly char[][] _buffer;
    private readonly Vector _dimensions;
    private readonly string[] _buttons = { "Continue", "Exit to Menu" };
    private int _selectedIndex;
    private int _pulseFrame;
    private readonly List<Vector> _stars;
    private readonly string[] _asciiTitle = new[]
    {
        @"__   __            ____  _          _ ",
        @"\ \ / /__  _   _  |  _ \(_) ___  __| |",
        @" \ V / _ \| | | | | | | | |/ _ \/ _` |",
        @"  | | (_) | |_| | | |_| | |  __/ (_| |",
        @"  |_|\___/ \__,_| |____/|_|\___|\__,_|"
    };


    public DeathMenu(Vector dimensions)
    {
        _dimensions = dimensions;
        _buffer = new char[_dimensions.Y][];
        _stars = Enumerable.Range(0, 50)
            .Select(_ => new Vector(Random.Shared.Next(_dimensions.X),
                                     Random.Shared.Next(_dimensions.Y)))
            .ToList();
        InitializeBuffer();
    }

    private void InitializeBuffer()
    {
        for (int y = 0; y < _dimensions.Y; y++)
        {
            _buffer[y] = new char[_dimensions.X];
            for (int x = 0; x < _dimensions.X; x++)
                _buffer[y][x] = ' ';
        }
    }

    public void Enter()
    {
        Console.Clear();
        DrawFrame();
        Render();
    }

    public void Update()
    {
        IsActive = true;
        var key = InputHandler.GetKeyDown();
        if (key == ConsoleKey.UpArrow)
            _selectedIndex = (_selectedIndex - 1 + _buttons.Length) % _buttons.Length;
        else if (key == ConsoleKey.DownArrow)
            _selectedIndex = (_selectedIndex + 1) % _buttons.Length;
        else if (key == ConsoleKey.Enter)
        {
            ActivateSelected();
            return;
        }

        UpdateStars();
        _pulseFrame = (_pulseFrame + 1) % 60;
        Console.ForegroundColor = _pulseFrame < 30 ? ConsoleColor.Red : ConsoleColor.DarkRed;
        DrawFrame();
        Render();
        Console.ResetColor();
    }

    public void Exit()
    {
        IsActive = false;
    }

    private void DrawFrame()
    {
        InitializeBuffer();
        DrawBorderLayer(0);
        DrawTitle();
        DrawButtons();
        DrawStars();
    }

    private void Render()
    {
        Console.SetCursorPosition(0, 0);
        for (int y = 0; y < _dimensions.Y; y++)
            Console.WriteLine(new string(_buffer[y]));
    }


    private void DrawBorderLayer(int layer)
    {
        char h = '═', v = '║', tl = '╔', tr = '╗', bl = '╚', br = '╝';
        int minX = layer, maxX = _dimensions.X - 1 - layer;
        int minY = layer, maxY = _dimensions.Y - 1 - layer;
        for (int x = minX; x <= maxX; x++)
        {
            _buffer[minY][x] = h;
            _buffer[maxY][x] = h;
        }
        for (int y = minY; y <= maxY; y++)
        {
            _buffer[y][minX] = v;
            _buffer[y][maxX] = v;
        }
        _buffer[minY][minX] = tl;
        _buffer[minY][maxX] = tr;
        _buffer[maxY][minX] = bl;
        _buffer[maxY][maxX] = br;
    }

    private void DrawTitle()
    {
        int startY = _dimensions.Y / 8;
        foreach (var (line, idx) in _asciiTitle.Select((l, i) => (l, i)))
        {
            int x = (_dimensions.X - line.Length) / 2;
            int y = startY + idx;
            DrawText(line, x, y);
        }
    }

    private void DrawButtons()
    {
        int startY = _dimensions.Y / 2;
        for (int i = 0; i < _buttons.Length; i++)
        {
            string text = _buttons[i];
            int x = (_dimensions.X - text.Length - 4) / 2;
            int y = startY + i * 2;
            string prefix = i == _selectedIndex ? "> " : "  ";
            DrawText(prefix + text, x, y);
        }
    }

    private void UpdateStars()
    {
        for (int i = 0; i < _stars.Count; i++)
        {
            var s = _stars[i];
            s.Y = (s.Y + 1) % _dimensions.Y;
            _stars[i] = s;
        }
    }

    private void DrawStars()
    {
        foreach (var s in _stars)
            _buffer[s.Y][s.X] = '.';
    }

    private void DrawText(string text, int x, int y)
    {
        if (y < 0 || y >= _dimensions.Y || x < 0) return;
        for (int i = 0; i < text.Length && x + i < _dimensions.X; i++)
            _buffer[y][x + i] = text[i];
    }

    private void ActivateSelected()
    {
        if (_selectedIndex == 0)
            SceneManager.SwitchTo(new SafeZoneScene());
        else
            SceneManager.SwitchTo(new MenuScene(_dimensions));
    }
}