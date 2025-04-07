using System.Numerics;

namespace CmdWalker;

internal class Collider
{
    private Vector _size;
    private Map _map;
    private GameEntity _parent;
    public Collider(Vector size, Vector position, GameEntity parent, Map map)
    {
        _size = size;
        _map = map;
        _parent = parent;
    }

    public GameEntity GetCollision()
    {
        foreach (var entity in _map.Entities)
        {
            if (entity.IsSelf(_parent.Position) && entity != _parent)
            {
                return entity;
            }
        }
        return null;
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
        Vector[] body = new Vector[_size.X];
        for (int x = 0; x < _size.X; x++)
        {
            body[x] = new Vector(_parent.Position.X + x, _parent.Position.Y);
        }
        return body;
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

    public bool CanMoveTo(Vector direction, Func<Vector, bool> isObstacle)
    {
        Vector[] positions = GetPositions();
        foreach (var pos in positions)
        {
            Vector newPos = pos + direction;
            if (newPos.X < 0 || newPos.Y < 0 || newPos.X >= _map.Size.X || newPos.Y >= _map.Size.Y) return false;
            if (IsOther(newPos) && isObstacle(newPos)) return false; 
        }
        return true;
    }
    
    private bool IsOther(Vector pos)
    {
        if (_map.GetCell(pos) == Blocks.GetGlyph(Block.Wall)) return true;

        foreach (var entity in _map.Entities)
        {
            if (entity is ICollectable) return false;
            if (entity.IsSelf(pos) && entity != _parent) return true;
        }
        return false;
    }
}