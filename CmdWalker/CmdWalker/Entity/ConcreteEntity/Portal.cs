namespace CmdWalker;

internal class Portal : GameEntity
{
    public readonly bool IsEntrance;
    private bool _isInitialized;
    private const ConsoleColor EntranceColor = ConsoleColor.Yellow;
    private const ConsoleColor ExitColor = ConsoleColor.Red;

    private int _tickCounter = 0;
    private int _ticksBeforeMove = 40;
    private Random _random = new();
    public Portal(Vector position, bool isEntrance = false) : base(position)
    {
        IsEntrance = isEntrance;
        Visual = new Sprite(
            RenderPalette.GetSprite(TileType.Portal),
            IsEntrance ? EntranceColor : ExitColor
        );
        Layer = 1;
    }

    public override void Update()
    {
        InitializeMapIfNeeded();
        _tickCounter++;
        if (_tickCounter < _ticksBeforeMove)
        {
            _map.SetCells(Transform.Position, Visual); 
            _tickCounter = 0;
        }
        CheckForCollisions();
    }

    private void InitializeMapIfNeeded()
    {
        if (_isInitialized) return;
        _map.SetCells(Transform.Position, Visual);
        Collider.IsTrigger = true; 
        _isInitialized = true;
    }

    private void CheckForCollisions()
    {
        foreach (var entity in _map.EntityManager.Entities.Where(e => e.Layer < Layer))
        {
            if (Collider.Intersects(entity.Collider))
            {
                _map.SetCells(Transform.Position, Visual); 
                if (IsEntrance && entity is Player)
                {
                    
                    SceneManager.SwitchTo(_random.Next(2) == 10 ? new RoomGameScene() : new DungeonGameScene(), LvlDifficult.Easy);
                    return; 
                }
            }
            
        }
    }
}