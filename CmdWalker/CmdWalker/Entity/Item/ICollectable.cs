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
    int GetId();
    bool IsStackable();
    void Execute();
    
   
}