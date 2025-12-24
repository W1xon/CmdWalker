namespace CmdWalker;

internal class Collider
{
    public bool IsTrigger { get; set; }
    public GameEntity Parent { get; set; }
    private Vector _size => Parent.Transform.Size;
    private Map _map;
    private bool _isSprite;
    public List<Type> Excludes = new List<Type>();
    
    public Collider(bool isTrigger = false)
    {
        IsTrigger = isTrigger;
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
    public void SetMap(Map map)
    {
        _map = map;
    }
    public int GetDistance(Vector dir)
    {
        if (dir is { X: 0, Y: 0 })
            return 0;

        Vector startPos = Parent.Transform.Position;
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

    public bool ContainsPoint(Vector pos)
    {
        Vector topLeft = Parent.Transform.Position;
        Vector bottomRight = Parent.Transform.Position + _size - Vector.One;
        
        return pos.X >= topLeft.X && pos.X <= bottomRight.X &&
                  pos.Y >= topLeft.Y && pos.Y <= bottomRight.Y;
    }
    public bool Intersects(Collider other)
    {
        return Intersects(Parent.Transform.Position, _size, other.Parent.Transform.Position, other._size);
    }
    public Vector[] GetPositions()
    {
        var body = new Vector[_size.X * _size.Y];
        int i = 0;

        for (int x = 0; x < _size.X; x++)
        for (int y = 0; y < _size.Y; y++)
            body[i++] = new Vector(Parent.Transform.Position.X + x, Parent.Transform.Position.Y + y);
        
        return body;
    }
    public Vector GetRelativePosition(Vector direction)
    {
        return direction switch
        {
            Vector v when v == Vector.Down => Parent.Transform.Position + Vector.Down,
            Vector v when v == Vector.Up => Parent.Transform.Position + Vector.Up,
            Vector v when v == Vector.Left => new Vector(Parent.Transform.Position.X - 1, Parent.Transform.Position.Y),
            Vector v when v == Vector.Right => new Vector(Parent.Transform.Position.X + Parent.Visual.Representation.Length, Parent.Transform.Position.Y),
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
        foreach (var entity in _map.EntityManager.Entities)
        {
            if (Excludes.Contains(entity.GetType())) return false;
            if (entity is ICollectable item && item.GetState() != ItemState.Active) continue;
            if (entity.IsSelf(pos) && entity != Parent && !entity.Collider.IsTrigger) return true;
        }
        return false;
    }
    
}