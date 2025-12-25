namespace CmdWalker
{
    internal abstract class GameEntity
    {
        public Transform Transform
        {
            get
            {
                if (_transform.Size != Visual.Size)
                {
                    _transform.Size = Visual.Size;
                    return _transform;
                }
                return _transform;
            }
            private set => _transform = value;
        }

        public IVisual Visual { get;  set; }
        public Collider Collider { get; private set; }
        public int Layer { get; protected set; }
        
        protected Map _map;
        private Transform _transform;
        public virtual void Update() { }
        public GameEntity(Vector position)
        {
            Transform = new Transform(position, Vector.One);
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
    }
}
