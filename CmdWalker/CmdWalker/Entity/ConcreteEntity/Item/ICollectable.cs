namespace CmdWalker;

public enum ItemState
{
    OnMap,    
    InInventory, 
    Active    
}

public interface ICollectable
{
    string GetName();
    IVisual GetVisual();
    int GetId();
    ItemState GetState();
    bool IsStackable();
    void Execute();
}