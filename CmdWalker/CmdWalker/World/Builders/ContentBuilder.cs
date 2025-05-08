namespace CmdWalker;

internal enum LvlTypeGeneration
{
    BSP,
    SafeZone,
}
internal abstract class ContentBuilder(LvlConfig config)
{
    
    protected MapContent Content = new();
    protected LvlConfig _config = config;
    protected TileMap _tileMap;
    public abstract void AddCarcassBuilder();
    public abstract  void AddEntities();
    public abstract void AddItems();
    public abstract void AddUnits();
    public abstract void SetConfig();
    public void SetTileMap(TileMap tileMap) => _tileMap  = tileMap;
    public MapContent GetTemplate() => Content;

}