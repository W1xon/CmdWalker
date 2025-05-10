namespace CmdWalker;

internal class Collider
{
    public bool IsTrigger { get; set; }
    public GameEntity Parent { get; set; }
    private Vector _size => Parent.GetSize();
    private Map _map;
    private bool _isSprite;
    
    public Collider(bool isTrigger = false)
    {
        IsTrigger = isTrigger;
    }
    public void SetMap(Map map)
    {
        _map = map;
    }
    public static bool Intersects(Vector posA, Vector sizeA, Vector posB, Vector sizeB)
    {
        Vector topLeftA = posA;
        Vector bottomRightA = posA + sizeA;

        Vector topLeftB = posB;
        Vector bottomRightB = posB + sizeB;

        bool overlapX = topLeftA.X < bottomRightB.X && bottomRightA.X > topLeftB.X;
        bool overlapY = topLeftA.Y < bottomRightB.Y && bottomRightA.Y > topLeftB.Y;

        return overlapX && overlapY;
    }
    public int GetDistance(Vector dir)
    {
        if (dir is { X: 0, Y: 0 })
            return 0;

        Vector startPos = Parent.Position;
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
            if (entity != Parent && Intersects(entity.Collider))
            {
                return entity;
            }
        }
        return null;
    }
    public bool ContainsPoint(Vector pos)
    {
        Vector topLeft = Parent.Position;
        Vector bottomRight = Parent.Position + _size;
        
        return pos.X >= topLeft.X && pos.X <= bottomRight.X &&
                  pos.Y >= topLeft.Y && pos.Y <= bottomRight.Y;
    }
    public bool Intersects(Collider other)
    {
        return Intersects(Parent.Position, _size, other.Parent.Position, other._size);
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
                    if(Parent.IsSelf(cellPosition)) continue;
                        
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
            body[i++] = new Vector(Parent.Position.X + x, Parent.Position.Y + y);
        
        return body;
    }
    public Vector GetRelativePosition(Vector direction)
    {
        return direction switch
        {
            Vector v when v == Vector.down => Parent.Position + Vector.down,
            Vector v when v == Vector.up => Parent.Position + Vector.up,
            Vector v when v == Vector.left => new Vector(Parent.Position.X - 1, Parent.Position.Y),
            Vector v when v == Vector.right => new Vector(Parent.Position.X + Parent.Visual.Representation.Length, Parent.Position.Y),
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
            if (entity.IsSelf(pos) && entity != Parent && !entity.Collider.IsTrigger) return true;
        }

        return false;
    }
    
}