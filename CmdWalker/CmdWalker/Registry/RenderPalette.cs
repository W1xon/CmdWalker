namespace CmdWalker
{
    internal enum TileType
    {
        Floor,
        Wall,
        Skeleton,
        Portal
    }
    internal static class RenderPalette
    {
        private static readonly Dictionary<TileType, string> _glyphs = new()
        {
            { TileType.Floor, "·" },       
            { TileType.Wall, "█" },        
            { TileType.Skeleton, "[x_x]" },
        };

        private static readonly Dictionary<TileType, char[,]> _sprites = new()
        {
            {
                TileType.Portal, new char[,]
                {
                    { '*', 'O', '*' },
                    { '~', '*', ' ' },
                    { '~', '*', '~' },
                }
            },
        };
        
        public static char GetChar(TileType tileType) =>  _glyphs[tileType][0];
        public static string GetString(TileType tileType) =>  _glyphs[tileType];

        public static char[,] GetSprite(TileType tileType) => _sprites[tileType];
    }
}
