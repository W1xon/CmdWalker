namespace CmdWalker;

internal class CellularCarcassGenerator : CarcassGenerator
{
    private const int SmoothIterations = 5;

    private int FillPercent = 40;
    public CellularCarcassGenerator(LvlConfig config) : base(config)
    {
        FillPercent = _rand.Next(40, 48);
    }

    public override char[,] Generate()
    {
        RandomFillMap();
            
        for (int i = 0; i < SmoothIterations; i++)
            SmoothMap();
            
        KeepOnlyMainRoom();
        AddWallBorders();
            
        return _field;
    }

    private void RandomFillMap()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                bool isEdge = x == 0 || x == _width - 1 || y == 0 || y == _height - 1;
                bool isWall = isEdge || _rand.Next(100) < FillPercent;
                    
                _field[y, x] = RenderPalette.GetChar(isWall ? TileType.Wall : TileType.Floor);
            }
        }
    }

    private void SmoothMap()
    {
        char[,] newMap = new char[_height, _width];
            
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                int wallCount = CountSurroundingWalls(x, y);
                    
                if (wallCount != 4)
                    newMap[y, x] = RenderPalette.GetChar(wallCount > 4 ? TileType.Wall : TileType.Floor);
                else
                    newMap[y, x] = _field[y, x];
            }
        }
            
        _field = newMap;
    }

    private int CountSurroundingWalls(int x, int y)
    {
        int count = 0;
            
        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dy = -1; dy <= 1; dy++)
            {
                if (dx == 0 && dy == 0) continue;
                    
                int nx = x + dx;
                int ny = y + dy;
                    
                bool outOfBounds = nx < 0 || nx >= _width || ny < 0 || ny >= _height;
                    
                if (outOfBounds || _field[ny, nx] == RenderPalette.GetChar(TileType.Wall))
                    count++;
            }
        }
            
        return count;
    }

    private void KeepOnlyMainRoom()
    {
        var rooms = FindAllRooms();
        var mainRoom = FindLargestRoom(rooms);
            
        FillAllRoomsExcept(rooms, mainRoom);
    }

    private List<List<Vector>> FindAllRooms()
    {
        var rooms = new List<List<Vector>>();
        var visited = new bool[_width, _height];
        char floorChar = RenderPalette.GetChar(TileType.Floor);
            
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                if (!visited[x, y] && _field[y, x] == floorChar)
                {
                    var room = FloodFill(x, y, floorChar, visited);
                    rooms.Add(room);
                }
            }
        }
            
        return rooms;
    }

    private List<Vector> FloodFill(int startX, int startY, char targetChar, bool[,] visited)
    {
        var tiles = new List<Vector>();
        var queue = new Queue<Vector>();
            
        queue.Enqueue(new Vector(startX, startY));
        visited[startX, startY] = true;
            
        Vector[] directions = { Vector.Up, Vector.Down, Vector.Left, Vector.Right };
            
        while (queue.Count > 0)
        {
            var tile = queue.Dequeue();
            tiles.Add(tile);
                
            foreach (var dir in directions)
            {
                int x = tile.X + dir.X;
                int y = tile.Y + dir.Y;
                    
                if (x >= 0 && x < _width && y >= 0 && y < _height && 
                    !visited[x, y] && _field[y, x] == targetChar)
                {
                    visited[x, y] = true;
                    queue.Enqueue(new Vector(x, y));
                }
            }
        }
            
        return tiles;
    }

    private List<Vector> FindLargestRoom(List<List<Vector>> rooms)
    {
        List<Vector> largest = new List<Vector>();
            
        foreach (var room in rooms)
            if (room.Count > largest.Count)
                largest = room;
            
        return largest;
    }

    private void FillAllRoomsExcept(List<List<Vector>> rooms, List<Vector> mainRoom)
    {
        char wallChar = RenderPalette.GetChar(TileType.Wall);
            
        foreach (var room in rooms)
        {
            if (room != mainRoom)
            {
                foreach (var tile in room)
                    _field[tile.Y, tile.X] = wallChar;
            }
        }
    }

    private void AddWallBorders()
    {
        char wallChar = RenderPalette.GetChar(TileType.Wall);
            
        for (int x = 0; x < _width; x++)
        {
            _field[0, x] = wallChar;
            _field[_height - 1, x] = wallChar;
        }
            
        for (int y = 0; y < _height; y++)
        {
            _field[y, 0] = wallChar;
            _field[y, _width - 1] = wallChar;
        }
    }
}