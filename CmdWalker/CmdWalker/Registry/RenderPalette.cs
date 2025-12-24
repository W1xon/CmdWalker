namespace CmdWalker
{
    internal enum TileType
    {
        Floor,
        Wall,
        Skeleton,
        Portal,
        Player,
        Gun,
        Bullet,
        BounceBullet
    }
    internal static class RenderPalette
    {
        private static readonly Dictionary<TileType, string> _glyphs = new()
        {
            { TileType.Floor, "." },       
            { TileType.Wall, "█" },        
            { TileType.Skeleton, "[x_x]" },
            { TileType.Player, ";)" },
            { TileType.Gun, "\u2566" },
            { TileType.Bullet, "*" },
            { TileType.BounceBullet, "+" },
        };

        private static readonly Dictionary<TileType, char[,]> _sprites = new()
        {
            {
                TileType.Portal, new char[,]
		        {
   			        { '╔', '═', '╦', '═', '╗' },
   			        { '║', ' ', 'O', ' ', '║' },
   			        { '╚', '═', '╩', '═', '╝' },
		        }
            },
        };
        
        public static char GetChar(TileType tileType) =>  _glyphs[tileType][0];
        public static string GetString(TileType tileType) =>  _glyphs[tileType];

        public static Vector GetSize(TileType tileType)
        {
            if (_glyphs.TryGetValue(tileType, out var glyph))
            {
                return new Vector(glyph.Length, 1);
            }
            else if (_sprites.TryGetValue(tileType, out var sprite))
            {
                return new Vector(sprite.GetLength(1), sprite.GetLength(0));
            }
            return Vector.One;
        }
        public static char[,] GetSprite(TileType tileType) => _sprites[tileType];
    }
}
