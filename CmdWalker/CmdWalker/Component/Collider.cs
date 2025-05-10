namespace CmdWalker;

internal class Collider
{
    public bool IsTrigger { get; set; }
    private Vector _size => _parent.GetSize();
    private Map _map;
    private GameEntity _parent;
    private bool _isSprite;
    
    public Collider(Vector position, GameEntity parent, Map map, bool isTrigger = false)
    {
        _map = map;
        IsTrigger = isTrigger;
        _parent = parent;
    }

    public int GetDistance(Vector dir)
    {
        if (dir is { X: 0, Y: 0 })
            return 0;

        Vector startPos = _parent.Position;
        Vector currentPos = startPos;
        Vector step = new Vector(Math.Sign(dir.X), Math.Sign(dir.Y));

        while (_map.IsWithinBounds(currentPos))
        {
            currentPos += step;

            if (_map.GetCell(currentPos, true) != RenderPalette.GetChar(TileType.Floor))
            {
                Vector relativePos = GetRelativePosition(dir);
                return Vector.Distance(relativePos, currentPos);
            }
        }
        return 0;
    }

    public GameEntity GetCollision()
    {
        foreach (var entity in _map.Entities)
        {
            if (entity != _parent && Intersects(entity.Collider))
            {
                return entity;
            }
        }
        return null;
    }
    public bool ContainsPoint(Vector pos)
    {
        Vector topLeft = _parent.Position;
        Vector bottomRight = _parent.Position + _size;
        
        return pos.X >= topLeft.X && pos.X <= bottomRight.X &&
                  pos.Y >= topLeft.Y && pos.Y <= bottomRight.Y;
    }

    public bool Intersects(Collider other)
    {
        Vector topLeftA = _parent.Position;
        Vector bottomRightA = _parent.Position + _size;

        Vector topLeftB = other._parent.Position;
        Vector bottomRightB = other._parent.Position + other._size;

        bool overlapX = topLeftA.X < bottomRightB.X && bottomRightA.X > topLeftB.X;
        bool overlapY = topLeftA.Y < bottomRightB.Y && bottomRightA.Y > topLeftB.Y;

        return overlapX && overlapY;
    }
    public Dictionary<Vector, char> GetNearbyBlocks()
    {
        Vector[] body = GetPositions();
        Dictionary<Vector, char> neighbours = new Dictionary<Vector, char>();
        foreach (var cell in body)
        {
            for (int y = -1; y <= 1; y++)
            {
                for (int x = -1; x <= 1; x++)
                {
                    int newX = cell.X + x;
                    int newY = cell.Y + y;

                    Vector cellPosition = new Vector(newX, newY);
                    if (newX >= _map.Size.X || newX < 0 || newY >= _map.Size.Y || newY < 0) continue;
                    if(neighbours.ContainsKey(cellPosition)) continue;
                    if(_parent.IsSelf(cellPosition)) continue;
                        
                    neighbours.Add(cellPosition, _map.GetCell(cellPosition));
                }
            }
        }

        return neighbours;
    } 
    public Vector[] GetPositions()
    {
        var body = new Vector[_size.X * _size.Y];
        int i = 0;

        for (int x = 0; x < _size.X; x++)
        for (int y = 0; y < _size.Y; y++)
            body[i++] = new Vector(_parent.Position.X + x, _parent.Position.Y + y);
        
        return body;
    }
    public Vector GetRelativePosition(Vector direction)
    {
        return direction switch
        {
            Vector v when v == Vector.down => _parent.Position + Vector.down,
            Vector v when v == Vector.up => _parent.Position + Vector.up,
            Vector v when v == Vector.left => new Vector(_parent.Position.X - 1, _parent.Position.Y),
            Vector v when v == Vector.right => new Vector(_parent.Position.X + _parent.Visual.Representation.Length, _parent.Position.Y),
            _ => throw new ArgumentException("Invalid direction")
        };
    }
    public bool CanMoveTo(Vector direction)
    {
        Vector[] currentPositions = GetPositions();
        foreach (var pos in currentPositions)
        {
            Vector newPos = pos + direction;
            if (newPos.X < 0 || newPos.Y < 0 || newPos.X >= _map.Size.X || newPos.Y >= _map.Size.Y) return false;
            if (IsOther(newPos))
                return false;
        }
        return true;
    }
    
    private bool IsOther(Vector pos)
    {
        if (_map.GetCell(pos) == RenderPalette.GetChar(TileType.Wall)) return true;
        foreach (var entity in _map.Entities)
        {
            if (entity is ICollectable item && item.GetState() != ItemState.Active) continue;
            if (entity.IsSelf(pos) && entity != _parent && !entity.Collider.IsTrigger) return true;
        }

        return false;
    }
    
}