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
        }
        public void BindToMap(Map map)
        {
            _map = map;
            Collider = new Collider(Position, this, _map);
        }
        public bool IsSelf(Vector pos)
        {
            return Collider.ContainsPoint(pos);
        }

        public virtual Vector GetSize() => Visual.Size;
    }
}
