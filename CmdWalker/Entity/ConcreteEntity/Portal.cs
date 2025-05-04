namespace CmdWalker;

internal class Portal : GameEntity
{
    private bool _isIn;
    public Portal(Vector position, bool isIn = false) : base(position)
    {
        _isIn = isIn;
        Visual = new Sprite(new char[,]
        {
            {'*', 'O', '*'},
            {'~', '*', GlyphRegistry.GetChar(Entity.Floor)},
            {'~', '*', '~'},
        }, isIn ? ConsoleColor.Green : ConsoleColor.Red);
        
    }

    public override void Update()
    {
        if (!Collider.IsTrigger) 
            Collider.IsTrigger = true;

        _map.SetCells(Position, Visual);
        if (!_isIn) return;
        foreach (var entity in _map.Entities)
        {
            foreach (var cell in Collider.GetPositions())
            {
                
                if (entity.IsSelf(cell) && entity is Player)
                {
                    SceneManager.SwitchTo(new RoomGameScene());
                    return;
                }
            }
        }
    }
}