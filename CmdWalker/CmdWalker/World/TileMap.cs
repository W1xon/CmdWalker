namespace CmdWalker;

internal class TileMap
{
    public char[,] Tiles { get; private set; }
    public Vector Size { get; private set; }
    
    private Random _rand = new Random();
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
        destination.Size = Size;
    }
    public Vector FindFreeAreaPosition(Vector areaSize)
    {
        Vector position = Vector.Zero;
        while (true)
        {
            Vector randV = Vector.GetRandom().Abs();
            position.X =  randV.X *_rand.Next(0, Size.X);
            position.Y =  randV.Y *_rand.Next(0, Size.Y);
            if (IsFree(position, areaSize)) break;
        }
        return position;
    }
    public Vector FindFreeAreaPositionInZone(Vector areaSize, Vector center, Vector zoneSize)
    {
        Vector position = Vector.Zero;
        while (true)
        {
            Vector randV = Vector.GetRandom().Abs();

            int top = center.Y - zoneSize.Y;
            int bottom = center.Y + zoneSize.Y;
            int left = center.X - zoneSize.X;
            int right = center.X + zoneSize.X;
            position.X =  randV.X *_rand.Next(left, right);
            position.Y =  randV.Y *_rand.Next(top, bottom);
            if (IsFree(position, areaSize)) break;
        }
        return position;
    }
    public bool IsFree(Vector pos, Vector areaSize)
    {
        bool isFree = true;
        for (int x = 0; x < areaSize.X; x++)
        {
            for (int y = 0; y < areaSize.Y; y++)
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