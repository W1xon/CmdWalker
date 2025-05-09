using System.Text;

namespace CmdWalker
{
    internal static class ArrayExtensions
    {
        public static char[,] DeepCopy(this char[,] source)
        {
            if (source == null) return new char[1,1];
            var copy = new char[source.GetLength(0), source.GetLength(1)];
            for (int x = 0; x < source.GetLength(1); x++)
            {
                for (int y = 0; y < source.GetLength(0); y++)
                {
                    copy[y,x] = source[y,x];
                }
            }
            return copy;
        }
        public static void Paste(this char[,] source, char[,] target, Vector position)
        {
            if (target == null || source == null) return;
            if (source.GetLength(0) > target.GetLength(0) ||
                source.GetLength(1) > target.GetLength(1)) return;
            
            for (int x = 0; x < source.GetLength(1); x++)
            {
                for(int y = 0; y < source.GetLength(0); y++)
                {
                    if (source.GetLength(1) + position.X >= target.GetLength(1)) continue;
                    if (source.Length + position.Y >= target.Length) continue;
                    target[y + position.Y, x + position.X] = source[y, x];
                }
            }
        }

        public static void Fill(this char[,] source, char symbol)
        {
            for (int y = 0; y < source.GetLength(0); y++)
            {
                for (int x = 0; x < source.GetLength(1); x++)
                {
                    source[y, x] = symbol;
                }
            }
        }
        
        public static string GetRowAsString(this char[,] array, int row)
        {
            int cols = array.GetLength(1);
            var sb = new StringBuilder(cols);
            for (int i = 0; i < cols; i++)
            {
                sb.Append(array[row, i]);
            }
            return sb.ToString();
        }
        public static void InsertString(this char[,] array, int row, int startCol, string str)
        {
            int width = array.GetLength(1);
            for (int i = 0; i < str.Length && (startCol + i) < width; i++)
            {
                array[row, startCol + i] = str[i];
            }
        }

    }
}
