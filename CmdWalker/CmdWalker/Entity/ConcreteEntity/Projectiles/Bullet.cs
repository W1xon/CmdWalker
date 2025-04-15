namespace CmdWalker
{
    internal class Bullet : Projectile
    {

        public Bullet(Vector position,  ItemState state, Vector dir, GameEntity parent) : base(position, parent, state, dir)
        {
            _damage = 100;
            Glyph = new Glyph( "*", ConsoleColor.Red);
        }

        public Bullet(Vector position, ItemState state) : base(position)
        {
            _state = state;
            
            Glyph = new Glyph( "*", ConsoleColor.Cyan);
        }
        
        public override string GetName() => "Bullet";
        public override Glyph GetGlyph() => Glyph;

        public override int GetId() => 100;
        public override ItemState GetState() => _state;

        public override bool IsStackable() => true;
        public override void Execute() => _state = ItemState.Active; 
        public override void Update()
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
        }
        

        public override void Move(Vector direction)
        {
            ClearPreviousPosition();
            if (CanMoveDir(direction))
            {    
                Position += direction;
                _map.SetCells(Collider.GetPositions(), Glyph);
                return;
            }
            if(!TryKill())
                Destroy();
        }
    }
}
