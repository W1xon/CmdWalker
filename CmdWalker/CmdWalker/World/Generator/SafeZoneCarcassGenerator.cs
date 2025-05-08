namespace CmdWalker;

internal class SafeZoneCarcassGenerator : CarcassGenerator
{
    public SafeZoneCarcassGenerator(LvlConfig config) : base(config)
    {
    }

    public override char[,] Generate()
    {
        AddShapeContour(new Rectangle(Vector.zero, _config.Size - Vector.one), RenderPalette.GetChar(TileType.Wall));
        return _field;
    }
}