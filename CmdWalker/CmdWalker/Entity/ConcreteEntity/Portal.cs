namespace CmdWalker;

internal class Portal : GameEntity
{
    private readonly bool _isEntrance;
    private bool _isInitialized;
    private const ConsoleColor EntranceColor = ConsoleColor.Yellow;
    private const ConsoleColor ExitColor = ConsoleColor.Red;

    public Portal(Vector position, bool isEntrance = false) : base(position)
    {
        _isEntrance = isEntrance;
        Visual = new Sprite(
            RenderPalette.GetSprite(TileType.Portal),
            _isEntrance ? EntranceColor : ExitColor
        );
    }

    public override void Update()
    {
        InitializeMapIfNeeded();
        CheckForCollisions();
    }

    private void InitializeMapIfNeeded()
    {
        if (_isInitialized) return;
        _map.SetCells(Position, Visual);
        Collider.IsTrigger = true; 
        _isInitialized = true;
    }

    private void CheckForCollisions()
    {
        foreach (var entity in _map.Entities)
        {
            foreach (var cell in Collider.GetPositions())
            {
                if (entity.IsSelf(cell))
                {
                    _map.SetCells(Position, Visual); 
                    if (_isEntrance && entity is Player)
                    {
                        SceneManager.SwitchTo(new RoomGameScene());
                        return; 
                    }
                }
            }
        }
    }
}