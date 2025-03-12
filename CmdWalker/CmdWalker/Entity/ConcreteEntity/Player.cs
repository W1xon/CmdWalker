namespace CmdWalker
{
    internal class Player : Unit
    {
        public Player(Vector pos) : base(pos, ";)", ConsoleColor.Green) 
        {
            _health = new Health(100);
        }
        public  override void Update()
        {
            Execute(InputHandler.GetKeyDown());
            Move(InputHandler.GetCurrentDirection());
        }
        public void Execute(ConsoleKey key)
        {
           switch (key)
           {
               case ConsoleKey.I:
                    _map.SpawnEntity(new BounceBulletCreator(Vector.down, this).Create(Position));
                   break;
               case ConsoleKey.K:
                    _map.SpawnEntity(new BounceBulletCreator(Vector.up, this).Create(Position) );
                   break;
               case ConsoleKey.J:
                    _map.SpawnEntity(new BounceBulletCreator(Vector.left, this).Create(Position));
                   break;
               case ConsoleKey.L:
                    _map.SpawnEntity(new BounceBulletCreator(Vector.right, this).Create(Position));
                    break;
           }
        }
        public override void Move(Vector direction)
        {
            ClearPreviousPosition();
            if (CanMoveDir(direction)) Position += direction;
            _map.SetCells(GetPositions(), Glyph, BodyColor);
        }
        public override void Destroy()
        {
            ClearPreviousPosition();
            _map.DeleteEntity(this);
            ConsoleDebugView.DebugInfo = "Игрока грохнули";
        }
    }
}