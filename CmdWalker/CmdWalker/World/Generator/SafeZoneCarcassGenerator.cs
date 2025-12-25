namespace CmdWalker;

internal class SafeZoneCarcassGenerator : CarcassGenerator
{
    public SafeZoneCarcassGenerator(LvlConfig config) : base(config)
    {
    }

    public override char[,] Generate()
    {
        Fill(RenderPalette.GetChar(TileType.Floor));

        AddShapeContour(
            new Rectangle(Vector.Zero, _config.Size - Vector.One),
            RenderPalette.GetChar(TileType.Wall)
        );

        return _field;
    }
}