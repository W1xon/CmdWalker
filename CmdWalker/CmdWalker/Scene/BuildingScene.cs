namespace CmdWalker;

public class BuildingScene : GameScene
{
    public override void Enter(LvlDifficult lvlDifficult)
    {
        base.Enter();
        IsActive = true;
        LvlConfig lvlConfig = new LvlConfig(lvlDifficult);
        lvlConfig.Constructions.Add(HomeShope.Construction[0]);
        Map = _mapGenerator.Generate(new BuildingContentBuilder(lvlConfig));
        InitCanvas();
        MapVisualizer.AnimateReachableArea(Map, new Vector(0,0));
        
    }
    public override void Update()
    {
        base.Update();
        if (!IsActive) return;
        Debug.Show();
        Render();
    }
    public override void InitCanvas()
    {
        Vector sizeCanvas = new Vector(Map.Size.X + 2, 0) + Map.Size;
        _canvas = new Canvas(sizeCanvas);
        _canvas.AddChild(Map, Vector.Zero);
        _canvas.AddChild(_debug, new Vector(Map.Size.X + 2, 0));
    }
}