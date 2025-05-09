namespace CmdWalker
{
    internal  class Debug : RenderObject
    {
        public static Debug Instance;
        public static (char[,], ConsoleColor[]) InventoryInfo;
        public static string Info = ""; 
        private static int _killCount;
        private static int  _oldInventoryY = 0;
        private static char[,] _oldInventoryView;  
        public Debug()
        {
            Instance = this;
        }
        public static void Show()
        {
            Instance.Draw(Instance.Position, new string(' ',8) , Instance);
            Instance.Draw(Instance.Position, $"FPS: {Game.CurrentFPS}", Instance, ConsoleColor.DarkGreen);
            Instance.Draw(new Vector( Instance.Position.X , Instance.Position.Y + 2), $"Kills: {_killCount}", Instance, ConsoleColor.DarkRed);
            
            Instance.Draw(new Vector( Instance.Position.X , Instance.Position.Y + 3), $"Инфа дебага: {Info}", Instance, ConsoleColor.DarkRed);

            
            if(InventoryInfo.Item1 != null && _oldInventoryView != InventoryInfo.Item1)
            {
                    _oldInventoryView = InventoryInfo.Item1;
                    Instance.ClearInventory();
                    Instance.ShowInventory();
                
            }

            Console.ForegroundColor = ConsoleColor.White;
        }
        
        public static void AddKill()
        {
            _killCount++;
        }

        private void ShowInventory()
        {
            for (int stackIndex = 0; stackIndex < InventoryInfo.Item2.Length; stackIndex++)
            {
                Console.ForegroundColor = InventoryInfo.Item2[stackIndex];

                for (int i = 0; i < 3; i++) 
                {
                    int y = Position.Y + 4 + stackIndex * 3 + i;
                    int inventoryRow = stackIndex * 3 + i;

                    Instance.Draw(
                        new Vector(Position.X, y),
                        InventoryInfo.Item1.GetRowAsString(inventoryRow),
                        Instance,
                        InventoryInfo.Item2[stackIndex]
                    );
                }
            }

            if (InventoryInfo.Item2.Length > 0)
            {
                _oldInventoryY = InventoryInfo.Item1.GetLength(0);
            }
        }


        private  void ClearInventory()
        {
            for (int y = 0; y < _oldInventoryY; y++)
            {
                    int newY = Position.Y + 4 + y;
                    Instance.Draw(new Vector(Position.X, newY), new string(' ', InventoryInfo.Item1.GetLength(1)) , Instance);
                
            }
        }

        public override void Draw(Vector position, string symbol, RenderObject renderObject, ConsoleColor color = ConsoleColor.White)
        {
            _parent.Draw(position, symbol,  this, color);
        }
        
    }
}
