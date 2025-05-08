namespace CmdWalker
{
    internal  class Debug : RenderObject
    {
        public static Debug Instance;
        public static (char[][], ConsoleColor[]) InventoryInfo;
        public static string Info = ""; 
        private static Map _map;
        private static int _killCount;
        private static int _oldInventoryX = 0, _oldInventoryY = 0;
        public Debug()
        {
            Instance = this;
        }
        public static void SetMap(Map map)
        {
            _map = map;
        }
        public static void Show()
        {
            Instance.Draw(Instance.Position, $"Kills: {_killCount}", Instance, ConsoleColor.DarkRed);
            
            Instance.Draw(new Vector( Instance.Position.X , Instance.Position.Y + + 3), $"Инфа дебага: {Info}", Instance, ConsoleColor.DarkRed);

            
            if(InventoryInfo.Item1 != null)
            {
                Instance.ClearInventory();
                Instance.ShowInventory();
            }

            Console.ForegroundColor = ConsoleColor.White;
        }
        
        public static void AddKill()
        {
            _killCount++;
        }

        private  void ShowInventory()
        {
            for (int y = 0; y < InventoryInfo.Item1.Length; y++)
            {
                Console.ForegroundColor = InventoryInfo.Item2[y / 3];
                for (int x = 0; x < InventoryInfo.Item1[0].Length; x++)
                {
                    int newX = Position.X + x;
                    int newY = Position.Y + 4 + y;
                    Instance.Draw(new Vector(newX, newY), InventoryInfo.Item1[y][x].ToString(), Instance, InventoryInfo.Item2[y / 3]);

                }
            }
            
            if(InventoryInfo.Item1.Length > 0)
            {
                _oldInventoryX = InventoryInfo.Item1[0].Length;
                _oldInventoryY = InventoryInfo.Item1.Length;
            }
        }

        private  void ClearInventory()
        {
            for (int y = 0; y < _oldInventoryY; y++)
            {
                for (int x = 0; x < _oldInventoryX; x++)
                {
                    int newX = Position.X + x;
                    int newY = Position.Y + 4 + y;
                    Instance.Draw(new Vector(newX, newY), " ", Instance);
                }
            }
        }

        public override void Draw(Vector position, string symbol, RenderObject renderObject, ConsoleColor color = ConsoleColor.White)
        {
            _parent.Draw(position, symbol,  this, color);
        }
        
    }
}
