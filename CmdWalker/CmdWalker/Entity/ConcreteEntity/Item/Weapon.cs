namespace CmdWalker;

internal abstract class Weapon : GameEntity, ICollectable
{
    protected ItemState _state;
    protected Vector _dir;
    protected GameEntity _parent;
    public Weapon(Vector position, GameEntity parent,  ItemState state) : base(position)
    {
        _state = state;
        _parent = parent;
        Layer = 3;
    }
    public abstract string GetName();

    public abstract IVisual GetVisual();

    public abstract int GetId();
    
    public abstract bool IsStackable();
    public abstract void Execute();

    public abstract ItemState GetState();
    public abstract void Fire(Vector dir, Inventory inventory);
    
}