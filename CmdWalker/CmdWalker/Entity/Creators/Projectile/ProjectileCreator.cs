namespace CmdWalker;

internal abstract class ProjectileCreator : ICollectableCreator
{
    protected Vector _spawnPosition;
    protected Vector _dir;
    protected GameEntity _parent;
    public ProjectileCreator(){}
    public abstract ICollectable Create( );

    public abstract GameEntity CreateOnMap(Vector position);

    public abstract GameEntity CreateActive();
    public abstract void Set(GameEntity parent, Vector direction);

    protected void CalculateSpawnPosition()
    {
        if (_dir == Vector.down)
            _spawnPosition = _parent.Position + Vector.down;
        if (_dir == Vector.up)
            _spawnPosition = _parent.Position + Vector.up;
        if (_dir == Vector.left)
            _spawnPosition = new Vector(_parent.Position.X - 1, _parent.Position.Y);
        if (_dir == Vector.right)
            _spawnPosition = new Vector(_parent.Position.X + _parent.Glyph.Symbol.Length, _parent.Position.Y);
    }
}