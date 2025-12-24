namespace CmdWalker;

internal interface IVisual
{
    char[,] Representation { get; set; }    
    ConsoleColor Color { get; }
    Vector Size { get; }
    string LeftAdditive { get; set; }
    string RightAdditive { get; set; }
    string UpAdditive { get; set; }
    string DownAdditive { get; set; }
    
}