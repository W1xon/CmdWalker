namespace CmdWalker;

internal class Glyph : IVisual
{
    private readonly string _symbol;
    private readonly ConsoleColor _color;
    private string _leftAdditive;
    private string _rightAdditive;
    private char[,] _representation;

    public char[,] Representation
    {
        get => _representation;
        set => _representation = value;
    }

    public ConsoleColor Color => _color;

    public Vector Size => new Vector(_representation.GetLength(1), 1);

    public string LeftAdditive
    {
        get => _leftAdditive;
        set
        {
            value ??= string.Empty;
            if (_leftAdditive != value)
            {
                _leftAdditive = value;
                UpdateRepresentation();
            }
        }
    }

    public string RightAdditive
    {
        get => _rightAdditive;
        set
        {
            value ??= string.Empty;
            if (_rightAdditive != value)
            {
                _rightAdditive = value;
                UpdateRepresentation();
            }
        }
    }
    public string Symbol => $"{_leftAdditive}{_symbol}{_rightAdditive}";
    public Glyph(string symbol, ConsoleColor color)
    {
        _symbol = symbol;
        _color = color;
        _leftAdditive = string.Empty;
        _rightAdditive = string.Empty;
        UpdateRepresentation(); 
    }
    private void UpdateRepresentation()
    {
        string fullSymbol = $"{_leftAdditive}{_symbol}{_rightAdditive}";
        _representation = new char[1, fullSymbol.Length];
        for (int x = 0; x < fullSymbol.Length; x++)
        {
            _representation[0, x] = fullSymbol[x];
        }
    }
}