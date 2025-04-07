namespace CmdWalker;

internal class Glyph(string symbol, ConsoleColor color)
{
    public string Symbol { get; private set; } = symbol;
    public ConsoleColor Color { get; private set; } = color;

    public Vector Size => new(Symbol.Length, 1);
}