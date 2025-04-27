using System.Numerics;

namespace CmdWalker
{
    internal abstract class GameEntity
    {
        public Vector Position { get; set; }

        public IVisual Visual { get;  set; }
        public Collider Collider { get; private set; }
        
        protected Map _map;
        public virtual void Update() { }
        public GameEntity(Vector position)
        {
            Position = position;
        }
        public void BindToMap(Map map)
        {
            _map = map;
            Collider = new Collider(Position, this, _map);
        }
        public bool IsSelf(Vector pos)
        {
            foreach(var vector in Collider.GetPositions())
            {
                if (pos == vector) return true;
            }
            return false;
        }

        public virtual Vector GetSize() => Visual.Size;
    }
}
