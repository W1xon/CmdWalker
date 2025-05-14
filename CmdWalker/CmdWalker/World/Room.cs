namespace CmdWalker
{
    internal class Room : IStructure
    {
        public Transform Transform { get; set; }
        public char[,] Plane;
        private Door _door;
        private Random _rand = new Random();
        public Room(Transform transform)
        {
            Transform = transform;
            _door = new Door(this);
        }

        public void Build(Map map)
        {
            Create();
            
            Plane.Paste(map.Carcas.Tiles, Transform.Position);
        }
        public void Create()
        {
            Plane = new char[Transform.Size.Y, Transform.Size.X];
            for (int y = 0; y < Transform.Size.Y; y++)
            {
                for (int x = 0; x < Transform.Size.X; x++)
                {
                    if (IsWall(new Vector(x, y)))
                        Plane[y,x] = RenderPalette.GetChar(TileType.Wall);
                    else
                        Plane[y,x] = RenderPalette.GetChar(TileType.Floor);
                }
            }

            bool canDoor = false;
            for (int i = 0; i < 5; i++)
            {
                int chance = _rand.Next(0, 100);
                switch (i)
                {
                    case 0:
                        if (chance < 25)
                        {
                            canDoor = true;
                            _door.Create(Vector.up);
                        }
                        break;
                    case 1:
                        if (chance < 25)
                        {
                            canDoor = true;
                            _door.Create(Vector.down);
                        }
                        break;
                    case 2:
                        if (chance < 25)
                        {
                            canDoor = true;
                            _door.Create(Vector.left);
                        }
                        break;
                    case 3:
                        if (chance < 25)
                        {
                            canDoor = true;
                            _door.Create(Vector.right);
                        }
                        break;
                    default:
                        if (!canDoor)
                        {
                            _door.Create(Vector.right);
                        }
                        break;
                }
            }
        }
        protected bool IsWall(Vector position)
        {
            return (position.Y == 0 || position.X == 0) || (position.X == Transform.Size.X - 1 || position.Y == Transform.Size.Y - 1);
        }
    }
}
