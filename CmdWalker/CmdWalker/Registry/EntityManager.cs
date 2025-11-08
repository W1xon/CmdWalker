namespace CmdWalker;

internal class EntityManager
{
    public List<GameEntity> Entities { get => new List<GameEntity>(_entites); }
    private List<GameEntity> _entites = new List<GameEntity>();
    private Map _map;
    
    public EntityManager(Map map)
    {
        _map = map;
    }
    public void DeleteEntity(GameEntity entity)
    {
        if (!_entites.Contains(entity)) return;
        _entites.Remove(entity);
    }
    public void SpawnEntity(GameEntity entity)
    {
        if (TryAddEntity(entity))
        {
            entity.BindToMap(_map);
        }
    }
    private bool TryAddEntity(GameEntity entity)
    {
        if (_entites.Contains(entity) && entity == null) return false;
        _entites.Add(entity);
        return true;
    }
    public List<T> GetEntity<T>() where T : GameEntity
    {
        return _entites.OfType<T>().ToList();
    }
}