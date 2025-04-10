﻿namespace CmdWalker;

internal abstract class Weapon : GameEntity, ICollectable
{
    protected ItemState _state;
    protected Vector _dir;
    protected GameEntity _parent;
    public Weapon(Vector position, GameEntity parent,  ItemState state) : base(position)
    {
        _state = state;
        _parent = parent;
        
        Glyph = new Glyph("\u2566", ConsoleColor.Blue);
    }
    public abstract string GetName();

    public abstract Glyph GetGlyph();

    public abstract int GetId();
    
    public abstract bool IsStackable();
    public abstract void Execute();

    public abstract ItemState GetState();
    public abstract void Fire(Vector dir, Inventory inventory);
    
}