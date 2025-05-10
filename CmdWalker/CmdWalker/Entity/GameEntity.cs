namespace CmdWalker
{
    internal abstract class GameEntity
    {
        public Vector Position { get; set; }

        public IVisual Visual { get;  set; }
        public Collider Collider { get; private set; }
        public int Layer { get; protected set; }
        
        protected Map _map;
        public virtual void Update() { }
        public GameEntity(Vector position)
        {
            Position = position;
            Collider = new Collider();
            Collider.Parent = this;
        }
        public void BindToMap(Map map)
        {
            _map = map;
            Collider.SetMap(_map);
        }
        public bool IsSelf(Vector pos)
        {
            return Collider.ContainsPoint(pos);
        }
        public bool IsIntersection(Collider other)
        {
            return Collider.Intersects(other);
        }

        public virtual Vector GetSize() => Visual.Size;
    }
}
