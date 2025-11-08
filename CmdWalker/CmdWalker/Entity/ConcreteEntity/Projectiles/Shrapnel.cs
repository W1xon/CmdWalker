namespace CmdWalker;

internal class Shrapnel : Projectile
{
    private int _distance = 3;
    private int _tickCounter = 0;
    private int _ticksBeforeMove = 5;
    public Shrapnel(Vector position,  ItemState state, Vector dir, GameEntity parent) : base(position, parent, state, dir)
    {
        _damage = 100;
        Visual = new Glyph( "\u00a4", ConsoleColor.Red);
        Collider.IsTrigger = true;
    }
    public Shrapnel(Vector position, ItemState state) : base(position)
    {
        _state = state;
            
        Visual = new Glyph( "\u00a4", ConsoleColor.Cyan);
    }

    public override string GetName() => "Shrapnel";

    public override IVisual GetVisual() => Visual;

    public override int GetId() => 201;
    public override ItemState GetState() => _state;

    public override bool IsStackable() => false;
    public override void Execute() => _state = ItemState.Active;
    public override void Update()
    {
        _tickCounter++;
        if (_tickCounter > _ticksBeforeMove)
        {
            switch (_state)
            {
                case ItemState.OnMap:
                    UpdateOnMap();
                    break;
                case ItemState.Active:
                    UpdateActive();
                    break;
            }

            _tickCounter = 0;
        }
    }
    public override void Move(Vector direction)
    {
        _distance--;
        if (CanMoveDir(direction) && _distance > 0)
        {    
            ClearPreviousPosition();
            Transform.Position += direction;
            _map.SetCells(Transform.Position, Visual);
            return;
        }
        if(!TryKill() ||  _distance <= 0 )
        {
            Destroy();
        }
    }
}