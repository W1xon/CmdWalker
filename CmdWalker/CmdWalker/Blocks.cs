namespace CmdWalker
{
    public enum Block
    {
        Floor,
        Wall,
    }
    public enum Units
    {
        Player,
        Skillet,
    }
    internal static class Blocks
    {
        private static readonly Dictionary<Block, char> _glyphs = new Dictionary<Block, char>()
        {
            {Block.Floor, '.'},
            {Block.Wall, '#'},
        }; 
        private static readonly Dictionary<Units, string> _units = new Dictionary<Units, string>()
        {
            {Units.Player, "<>"},
            {Units.Skillet, "0_0"},
        };
        public static char GetGlyph(Block name) => _glyphs[name];
        public static string GetUnitGlyph(Units name) => _units[name];
        
    }
}
