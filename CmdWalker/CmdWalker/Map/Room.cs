namespace CmdWalker
{
    internal class Room : IStructure
    {
        public Vector Size { get; set; }
        public char[][] Plane;
        private Vector _position;
        private Door _door;
        private Random _rand = new Random();
        public Room(Vector position, Vector size)
        {
            _position = position;
            Size = size;
            _door = new Door(this);
        }

        public void Build(Map map)
        {
            Create();
            Plane.Paste(map.Carcas, _position);
        }
        public void Create()
        {
            Plane = new char[Size.Y][];
            for (int y = 0; y < Size.Y; y++)
            {
                Plane[y] = new char[Size.X];
                for (int x = 0; x < Size.X; x++)
                {
                    if (IsWall(new Vector(x, y)))
                        Plane[y][x] = Blocks.GetGlyph(Block.Wall);
                    else
                        Plane[y][x] = Blocks.GetGlyph(Block.Floor);
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
            return (position.Y == 0 || position.X == 0) || (position.X == Size.X - 1 || position.Y == Size.Y - 1);
        }
    }
}
