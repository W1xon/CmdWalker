namespace CmdWalker;

public abstract class Unit : GameEntity, IMovable, IDamageable, IDestroyable
{
    protected Health _health;

    public Unit(Vector position) : base(position)
    {
        Layer = 0;
    }
    public abstract void Destroy();
    public abstract void Move(Vector direction);
    public abstract bool CanMoveDir(Vector dir);
    public void TakeDamage(int damage)
    {
        _health.TryTakeDamage(damage);
        if (_health.IsZero) Destroy();
    }
}