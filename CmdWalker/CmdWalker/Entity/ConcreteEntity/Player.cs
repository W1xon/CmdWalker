namespace CmdWalker
{
    internal class Player : Unit
    {
        public Inventory Inventory = new Inventory();
        private BulletCreator _bulletCreator;
        
        public Player(Vector pos) : base(pos)
        {
            _bulletCreator = new BulletCreator();
            _health = new Health(100);
            
            
            Glyph = new Glyph(";)", ConsoleColor.Green);
        }
        public override void Update()
        {
            KeyHandler(InputHandler.GetKeyDown());
            Move(InputHandler.GetCurrentDirection());
        }

        private void KeyHandler(ConsoleKey key)
        {
           switch (key)
           {
               case ConsoleKey.I:
                   Inventory.Use(Vector.down);
                   break;
               case ConsoleKey.K:
                   Inventory.Use(Vector.up);
                   break;
               case ConsoleKey.J:
                   Inventory.Use(Vector.left);
                   break;
               case ConsoleKey.L:
                   Inventory.Use(Vector.right);
                   break;
               
               case ConsoleKey.D1:
                   Inventory.Equip(0);
                   break;
               case ConsoleKey.D2:
                   Inventory.Equip(1);
                   break;
               case ConsoleKey.D3:
                   Inventory.Equip(2);
                   break;
               case ConsoleKey.D4:
                   Inventory.Equip(3);
                   break;
               case ConsoleKey.D5:
                   Inventory.Equip(4);
                   break;
               case ConsoleKey.D6:
                   Inventory.Equip(5);
                   break;
               case ConsoleKey.D7:
                   Inventory.Equip(6);
                   break;
               case ConsoleKey.D8:
                   Inventory.Equip(7);
                   break;
               case ConsoleKey.D9:
                   Inventory.Equip(8);
                   break;
           }
        }
        public override void Move(Vector direction)
        {
            ClearPreviousPosition();
            if (CanMoveDir(direction)) Position += direction;
            _map.SetCells(Collider.GetPositions(), Glyph);
        }
        public override bool CanMoveDir(Vector dir)
        {
            return Collider.CanMoveTo(dir);
        }
        public override void Destroy()
        {
            ClearPreviousPosition();
            _map.DeleteEntity(this);
            ConsoleDebugView.DebugInfo = "Игрока грохнули";
        }
    }
}