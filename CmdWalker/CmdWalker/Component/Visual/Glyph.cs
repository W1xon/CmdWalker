namespace CmdWalker;

internal class Glyph : IVisual
{
    private readonly ConsoleColor _color;

    private string _leftAdditive = string.Empty;
    private string _rightAdditive = string.Empty;
    private string _upAdditive = string.Empty;
    private string _downAdditive = string.Empty;

    private char[,] _representation;
    
    public char[,] Representation
    {
        get => _representation;
        set => _representation = value;
    }

    public ConsoleColor Color => _color;

    public Vector Size => new(
        _representation.GetLength(1),
        _representation.GetLength(0)
    );

    public string LeftAdditive
    {
        get => _leftAdditive;
        set => SetAdditive(ref _leftAdditive, value);
    }

    public string RightAdditive
    {
        get => _rightAdditive;
        set => SetAdditive(ref _rightAdditive, value);
    }

    public string UpAdditive
    {
        get => _upAdditive;
        set => SetAdditive(ref _upAdditive, value);
    }

    public string DownAdditive
    {
        get => _downAdditive;
        set => SetAdditive(ref _downAdditive, value);
    }

    public string Symbol { get; }

    public Glyph(string symbol, ConsoleColor color)
    {
        Symbol = symbol;
        _color = color;
        UpdateRepresentation();
    }


    private void SetAdditive(ref string field, string value)
    {
        value ??= string.Empty;

        if (field == value)
            return;

        field = value;
        UpdateRepresentation();
    }

    private void UpdateRepresentation()
    {
        string middleLine = $"{_leftAdditive}{Symbol}{_rightAdditive}";
        int width = middleLine.Length;

        bool hasUp = !string.IsNullOrWhiteSpace(_upAdditive);
        bool hasDown = !string.IsNullOrWhiteSpace(_downAdditive);

        int height = 1 + (hasUp ? 1 : 0) + (hasDown ? 1 : 0);
        _representation = new char[height, width];


        int y = 0;

        if (hasUp)
            DrawAdditiveLine(y++, _upAdditive);

        DrawCentralLine(y++);

        if (hasDown)
            DrawAdditiveLine(y, _downAdditive);
    }


    private void DrawAdditiveLine(int y, string additive)
    {
        int offset = _leftAdditive.Length;

        for (int x = 0; x < _representation.GetLength(1); x++)
        {
            if (x < offset)
            {
                _representation[y, x] = ' ';
            }
            else if (x - offset < additive.Length)
            {
                _representation[y, x] = additive[x - offset];
            }
            else
            {
                _representation[y, x] = ' ';
            }
        }
    }

    private void DrawCentralLine(int y)
    {
        string line = $"{_leftAdditive}{Symbol}{_rightAdditive}";

        for (int x = 0; x < line.Length; x++)
        {
            _representation[y, x] = line[x];
        }
    }
}
