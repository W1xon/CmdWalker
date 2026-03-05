namespace CmdWalker;

public class Debug : RenderObject
{
    public static List<InventorySummary> InventoryView;
    public static string Info = "";

    private static int _killCount;

    private static int _prevInventoryHeight = 0;
    private static int _prevInventoryCount;
    private static int _prevEquippedIndex;

    private static IScene _currentScene;

    private static Debug _instance;

    private const int SlotHeight = 3;
    private const int InventoryOffset = 4;

    public Debug()
    {
        _instance = this;
    }

    public static void Show()
    {
        _instance.Write(_instance.Position, new string(' ', 8), _instance);
        _instance.Write(_instance.Position, $"FPS: {Game.CurrentFPS}", _instance, ConsoleColor.DarkGreen);

        _instance.Write(
            new Vector(_instance.Position.X, _instance.Position.Y + 2),
            $"Kills: {_killCount}",
            _instance,
            ConsoleColor.DarkRed
        );

        _instance.Write(
            new Vector(_instance.Position.X, _instance.Position.Y + 3),
            $"Инфа дебага: {Info}",
            _instance,
            ConsoleColor.DarkRed
        );

        if (InventoryView != null && InventoryView.Count > 0)
        {
            _instance.ClearInventory();
            _instance.RenderInventory();

            _prevInventoryCount = InventoryView.Count;
            _prevEquippedIndex = GetEquippedIndex();
        }

        if (_currentScene != SceneManager.ActiveScene)
            _currentScene = SceneManager.ActiveScene;

        Console.ForegroundColor = ConsoleColor.White;
    }

    public static void AddKill()
    {
        _killCount++;
    }

    private void RenderInventory()
    {
        for (int i = 0; i < InventoryView.Count; i++)
        {
            for (int row = 0; row < InventoryView[i].Size.Y; row++)
            {
                int y = Position.Y + InventoryOffset + i * InventoryView[i].Size.Y + row;

                _instance.Draw(
                    new Vector(Position.X, y),
                    InventoryView[i].Rows.GetRowAsString(row),
                    _instance,
                    InventoryView[i].IsEquip ? ConsoleColor.Red : ConsoleColor.White
                );
            }
        }

        if (InventoryView.Count > 0)
        {
            _prevInventoryHeight = InventoryView.Count * SlotHeight;
        }
    }

    private void ClearInventory()
    {
        if (!NeedsInventoryClear())
            return;

        for (int y = 0; y < _prevInventoryHeight; y++)
        {
            int newY = Position.Y + InventoryOffset + y;

            _instance.Draw(
                new Vector(Position.X, newY),
                new string(' ', InventoryView[0].Rows.GetLength(1)),
                _instance
            );
        }
    }

    private bool NeedsInventoryClear()
    {
        return _prevInventoryCount != InventoryView.Count
               || _prevEquippedIndex != GetEquippedIndex();
    }

    private static int GetEquippedIndex()
    {
        for (int i = 0; i < InventoryView.Count; i++)
        {
            if (InventoryView[i].IsEquip)
                return i;
        }

        return -1;
    }

    public override void Write(Vector position, string symbol, RenderObject renderObject, ConsoleColor color = ConsoleColor.White)
    {
        _parent.Write(position, symbol, this, color);
    }

    public override void Draw(Vector position, string symbol, RenderObject renderObject, ConsoleColor color = ConsoleColor.White)
    {
        _parent.Draw(position, symbol, this, color);
    }
}