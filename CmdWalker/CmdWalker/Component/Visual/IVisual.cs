namespace CmdWalker;

internal interface IVisual
{
    char[,] Representation { get; }
    ConsoleColor Color { get; }
    Vector Size { get; }
    string LeftAdditive { get; set; }
    string RightAdditive { get; set; }
    
}