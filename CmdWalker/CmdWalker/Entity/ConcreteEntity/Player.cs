namespace CmdWalker
{
    internal class Player : Unit
    {
        public Inventory Inventory = new Inventory();
        private BulletCreator _bulletCreator;
        
        public Player(Vector pos) : base(pos, ";)", ConsoleColor.Green)
        {
            _bulletCreator = new BulletCreator();
            _health = new Health(100);
            for (int i = 0; i < 1; i++)
            {
                var bullet = _bulletCreator.Create();
                Inventory.PickUp(bullet);
            }
            Inventory.Equip(0);
        }
        public  override void Update()
        {
            Execute(InputHandler.GetKeyDown());
            Move(InputHandler.GetCurrentDirection());
        }

        private void Execute(ConsoleKey key)
        {
            bool isFire = false;
           switch (key)
           {
               case ConsoleKey.I:
                   if(Inventory.Contains("Bullet"))
                   {
                       isFire = true;
                       Inventory.Drop(false);
                       _bulletCreator.Set(this, Vector.down);
                   }
                   break;
               case ConsoleKey.K:
                   if(Inventory.Contains("Bullet"))
                   {
                       isFire = true;
                       Inventory.Drop(false);
                       _bulletCreator.Set(this, Vector.up);
                   }
                   break;
               case ConsoleKey.J:
                   if(Inventory.Contains("Bullet"))
                   {
                       isFire = true;
                       Inventory.Drop(false);
                       _bulletCreator.Set(this, Vector.left);
                   }
                   break;
               case ConsoleKey.L:
                   if(Inventory.Contains("Bullet"))
                   {
                       isFire = true;
                        Inventory.Drop(false);
                        _bulletCreator.Set(this, Vector.right);
                   }
                   break;
           }
           if(isFire)
            _map.SpawnEntity(_bulletCreator.CreateActive());
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