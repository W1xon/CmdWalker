namespace CmdWalker;

internal class SafeZoneCarcassGenerator : CarcassGenerator
{
    public SafeZoneCarcassGenerator(MapTemplate template) : base(template)
    {
    }

    public override char[,] Generate()
    {
        AddShapeContour(new Rectangle(Vector.zero, _template.Size - Vector.one), GlyphRegistry.GetChar(Entity.Wall));
        return _field;
    }
}