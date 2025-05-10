namespace CmdWalker;

internal class TileMap
{
    public char[,] Tiles { get; set; }
    public Vector Size { get; private set; }
    public void SetCell(Vector pos, char symbol)
    {
        if(IsWithinBounds(pos))
            Tiles[pos.Y, pos.X] = symbol;
    }
    public char GetCell(Vector pos)
    {
        // Т.к. массив, то координаты инвертированны
        if(IsWithinBounds(pos))
            return Tiles[pos.Y, pos.X];
        return RenderPalette.GetChar(TileType.Wall);
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
    public Vector GetRandomFreePosition(Random rng)
    {
        List<Vector> freePositions = new();

        for (int y = 0; y < Size.Y; y++)
        {
            for (int x = 0; x < Size.X; x++)
            {
                Vector pos = new Vector(x, y);
                if (IsFree(pos))
                    freePositions.Add(pos);
            }
        }

        if (freePositions.Count == 0)
            throw new InvalidOperationException("Нет свободных позиций на карте.");

        return freePositions[rng.Next(freePositions.Count)];
    }

    public bool IsFree(Vector pos, Vector size)
    {
        bool isFree = true;
        for (int x = 0; x < size.X; x++)
        {
            for (int y = 0; y < size.Y; y++)
            {
                Vector newPos = pos + new Vector(x, y);
                
                isFree = IsWithinBounds(newPos) && GetCell(newPos) == RenderPalette.GetChar(TileType.Floor);
                if (!isFree) return false;
            }
        }
        return isFree;
    }
    public bool IsFree(Vector pos)
    {
        return GetCell(pos) == RenderPalette.GetChar(TileType.Floor);
    }
    public bool IsWithinBounds(Vector pos)
    {
        return pos.X >= 0 && pos.X < Size.X && pos.Y >= 0 && pos.Y < Size.Y;
    }
}