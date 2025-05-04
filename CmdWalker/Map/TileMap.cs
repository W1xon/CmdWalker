namespace CmdWalker;

internal class TileMap
{
    public char[,] Tiles { get; set; }
    public Vector Size { get; private set; }
    public void SetCell(Vector pos, char symbol)
    {
        try
        {

            Tiles[pos.Y, pos.X] = symbol;
        }
        catch (Exception e)
        {
            Tiles[pos.Y, pos.X] = symbol;
            Console.WriteLine(e);
            throw;
        }
    }
    public char GetCell(Vector pos)
    {
        // Т.к. массив, то координаты инвертированны
        return Tiles[pos.Y, pos.X];
    }

    public void SetTile(char[,] tiles)
    {
        Tiles = tiles.DeepCopy();
        Size = new Vector(Tiles.GetLength(1), Tiles.GetLength(0));
    }
    
    public void CopyTo(TileMap destination)
    {
        destination.Tiles = Tiles.DeepCopy();
        destination.Size = this.Size;
    }

    public TileMap MergeWith(TileMap other)
    {
        TileMap result = new TileMap();
        other.CopyTo(result);
        for (int y = 0; y < Size.Y; y++)
        {
            for (int x = 0; x < Size.X; x++)
            {
                Vector pos = new Vector(x, y);
                char mergedChar = GetCell(pos) != other.GetCell(pos) ? other.GetCell(pos) : GetCell(pos);
                result.SetCell(pos, mergedChar);
            }
        }
        return result;
    }

}