namespace CmdWalker
{
    internal class Player : Unit
    {
        public readonly Inventory Inventory;
        
        public Player(Vector pos) : base(pos)
        {
            _health = new Health(100);
            Visual = new Glyph(RenderPalette.GetString(TileType.Player), ConsoleColor.Green);
            Inventory = new Inventory(this);
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
                   Inventory.TryUse(Vector.down, collectable =>
                       collectable.GetVisual().Size.X <= Collider.GetDistance(Vector.down));
                   break;
               case ConsoleKey.K:
                   Inventory.TryUse(Vector.up, collectable =>
                       collectable.GetVisual().Size.X <= Collider.GetDistance(Vector.up));
                   break;
               case ConsoleKey.J:
                   Inventory.TryUse(Vector.left, collectable =>
                       collectable.GetVisual().Size.X <= Collider.GetDistance(Vector.left));
                   break;
               case ConsoleKey.L:
                   Inventory.TryUse(Vector.right, collectable =>
                       collectable.GetVisual().Size.X <= Collider.GetDistance(Vector.right));
                   break;
               
               case ConsoleKey.D1:
                   Inventory.TryEquip(0, collectable =>
                       collectable.GetVisual().Size.X <= Collider.GetDistance(Vector.right));
                   break;
               case ConsoleKey.D2:
                   Inventory.TryEquip(1, collectable =>
                       collectable.GetVisual().Size.X <= Collider.GetDistance(Vector.right));
                   break;
               case ConsoleKey.D3:
                   Inventory.TryEquip(2, collectable =>
                       collectable.GetVisual().Size.X <= Collider.GetDistance(Vector.right));
                   break;
               case ConsoleKey.D4:
                   Inventory.TryEquip(3, collectable =>
                       collectable.GetVisual().Size.X <= Collider.GetDistance(Vector.right));
                   break;
               case ConsoleKey.D5:
                   Inventory.TryEquip(4, collectable =>
                       collectable.GetVisual().Size.X <= Collider.GetDistance(Vector.right));
                   break;
               case ConsoleKey.D6:
                   Inventory.TryEquip(5, collectable =>
                       collectable.GetVisual().Size.X <= Collider.GetDistance(Vector.right));
                   break;
               case ConsoleKey.D7:
                   Inventory.TryEquip(6, collectable =>
                       collectable.GetVisual().Size.X <= Collider.GetDistance(Vector.right));
                   break;
               case ConsoleKey.D8:
                   Inventory.TryEquip(7, collectable =>
                       collectable.GetVisual().Size.X <= Collider.GetDistance(Vector.right));
                   break;
               case ConsoleKey.D9:
                   Inventory.TryEquip(8, collectable =>
                       collectable.GetVisual().Size.X <= Collider.GetDistance(Vector.right));
                   break;
           }
        }
        public override void Move(Vector direction)
        {
            ClearPreviousPosition();
            if (CanMoveDir(direction)) Transform.Position += direction;
            _map.SetCells(Transform.Position, Visual);
        }
        public override bool CanMoveDir(Vector dir)
        {
            return Collider.CanMoveTo(dir);
        }
        public override void Destroy()
        {
            ClearPreviousPosition();
            _map.DeleteEntity(this);
            Debug.Info = "Игрока грохнули";
            SceneManager.SwitchTo(new DeathMenu(new Vector(90,30)));
        }
    }
}