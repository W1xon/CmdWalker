namespace CmdWalker;

internal class Portal : GameEntity
{
    public Portal(Vector position) : base(position)
    {
        Visual = new Sprite(new char[,]
        {
            {'*', 'O', '*'},
            {'~', '*', GlyphRegistry.GetChar(Entity.Floor)},
            {'~', '*', '~'},
        }, ConsoleColor.Red);
        
    }

    public override void Update()
    {
        if (!Collider.IsTrigger) 
            Collider.IsTrigger = true;
        _map.SetCells(Position, Visual);
        foreach (var entity in _map.Entities)
        {
            foreach (var cell in Collider.GetPositions())
            {
                
                if (entity.IsSelf(cell) && entity is Player)
                {
                    SceneManager.SwitchTo(new GameScene());
                    return;
                }
            }
        }
    }
}