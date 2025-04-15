namespace CmdWalker
{
    internal enum Entity
    {
        Floor,
        Wall,
        Skeleton,
    }
    internal static class GlyphRegistry
    {
        private static readonly Dictionary<Entity, string> _glyphs = new()
        {
            { Entity.Floor, "·" },       
            { Entity.Wall, "█" },        
            { Entity.Skeleton, "[x_x]" },
        };


        public static char GetChar(Entity entity)
        {
            return  _glyphs[entity][0];
        }

        public static string GetString(Entity entity)
        {
            return _glyphs[entity];
        }

    }
}
