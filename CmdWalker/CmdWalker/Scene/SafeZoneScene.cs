﻿namespace CmdWalker;

internal class SafeZoneScene : GameScene
{
    public override bool IsActive { get; set; }
    private Canvas _canvas = new Canvas();
    private Debug _debug = new Debug();
    public override void Enter()
    {
        base.Enter();
        IsActive = true;
        MapBuilder mapBuilder = new MapBuilder();
        MapGenerator mapGenerator = new MapGenerator(mapBuilder);
        LvlConfig lvlConfig = new LvlConfig(LvlDifficult.Easy);
        Map = mapGenerator.Generate(new SafeZoneContentBuilder(lvlConfig));
        
        InitCanvas();
        Map.Show();
    }
    public override void Update()
    {
        base.Update();
        if (!IsActive) return;
        Debug.Show();
    }
    public override void InitCanvas()
    {
        _canvas.AddChild(Map, Vector.zero);
        _canvas.AddChild(_debug, new Vector(Map.Size.X + 2, 0));
    }
}