namespace CmdWalker;

public class Table : Construction
{
    public Table(Vector position) : base(position)
    {
        Visual = new Sprite(
            RenderPalette.GetSprite(TileType.Table),
            ConsoleColor.Yellow
        );
        Layer = 1;
    }

    public override void EnterPlacementMode()
    {
        Visual.Color = ConsoleColor.DarkYellow;
        BaseEnterPlacementMode(); 
    }

    public override void ExitPlacementMode()
    {
        Visual.Color = ConsoleColor.Yellow;
        BaseExitPlacementMode(); 
        SceneManager.SwitchTo(new SafeZoneScene(), LvlDifficult.Easy);
    }

    protected override void OnConfirmPlacement()
    {
        HomeData.Construction.Add(this);
        HomeShope.Construction.Remove(this);
        ExitPlacementMode();
    }

    protected override void OnCancelPlacement()
    {
        ExitPlacementMode();
    }

    public override void Update()
    {
        Render();
    }

    private void Render()
    {
        _map.SetCells(Transform.Position, Visual);
    }
}