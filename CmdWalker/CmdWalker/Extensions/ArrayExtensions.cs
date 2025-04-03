namespace CmdWalker
{
    internal static class ArrayExtensions
    {
        public static char[][] DeepCopy(this char[][] source)
        {
            if (source == null) return new char[1][];
            var copy = new char[source.Length][];
            for (int i = 0; i < source.Length; i++)
            {
                if (source[i] != null) 
                {
                    copy[i] = new char[source[i].Length];
                    Array.Copy(source[i], copy[i], source[i].Length);
                }
            }
            return copy;
        }
        public static void Paste(this char[][] source, char[][] target, Vector position)
        {
            if (target == null || source == null) return;
            if (source.Length > target.Length || source[0].Length > target[0].Length) return;
            for (int x = 0; x < source[0].Length; x++)
            {
                for(int y = 0; y < source.Length; y++)
                {
                    if (source[y].Length + position.X >= target[0].Length) continue;
                    if (source.Length + position.Y >= target.Length) continue;
                    target[y + position.Y][x + position.X] = source[y][x];
                }
            }
        }
    }
}
