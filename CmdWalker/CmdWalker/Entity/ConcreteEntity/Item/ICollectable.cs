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
    IVisual GetVisual();
    int GetId();
    ItemState GetState();
    bool IsStackable();
    void Execute();
}