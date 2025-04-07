namespace CmdWalker;

internal enum ItemState
{
    OnMap,    
    InInventory, 
    Active    
}

internal interface ICollectable
{
    string GetName();
    Glyph GetGlyph();
    int GetId();
    ItemState GetState();
    bool IsStackable();
    void Execute();
}