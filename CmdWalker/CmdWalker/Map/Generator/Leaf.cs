namespace CmdWalker;

internal class Leaf
{
    public int Width => _size.X;
    public int Height => _size.Y;
    public int X => _position.X;
    public int Y => _position.Y;
    public Leaf leftChild;
    public Leaf rightChild;
    public Rectangle room;
    public List<Rectangle> halls;

    private Vector _position, _size;
    private Random _rand = new Random();
    private const int MIN_LEAF_SIZE = 10;
    public Leaf(Vector position, Vector size)
    {
        _position = position;
        _size = size;
    }
    public bool Split()
    {
        if (leftChild != null || rightChild != null)
            return false;
        bool splitH = _rand.NextSingle() > 0.25;
        float mult = 3f;
        if (Width > Height * mult && Width / (Height * mult) >= 1.25) 
            splitH = false; 
        else if (Height * mult > Width && (Height * mult) / Width >= 1.25) 
            splitH = true; 

        int max = (splitH ? Height : Width) - MIN_LEAF_SIZE;
        if (max <= MIN_LEAF_SIZE)
            return false;

        int split = _rand.Next(MIN_LEAF_SIZE, max);
            
        if (splitH)
        {
            leftChild = new Leaf(_position, new Vector(Width, split));
            rightChild = new Leaf(new Vector(X, Y + split),
                new Vector(Width, Height - split));

        }
        else
        {
            leftChild = new Leaf(_position, new Vector(split, Height));
            rightChild = new Leaf(new Vector(X + split, Y),
                new Vector(Width - split, Height));

        }
        return true;
    }
    public void CreateRooms()
    {
        if (leftChild != null || rightChild != null)
        {
            if (leftChild != null)
                leftChild.CreateRooms();
            if (rightChild != null)
                rightChild.CreateRooms();
            if(leftChild != null && rightChild != null)
            {
                CreateHall(leftChild.GetRoom(), rightChild.GetRoom());
            } 
        }
        else
        {
            int min = 8;
            Vector roomSize = new Vector(_rand.Next(min, Width - 2), _rand.Next(min, Height - 2));
            Vector roomPos = new Vector(_rand.Next(1, Width - roomSize.X - 1), _rand.Next(1, Height - roomSize.Y - 1));
            room = new Rectangle(new Vector(X + roomPos.X, Y + roomPos.Y),
                new Vector(  roomSize.X, roomSize.Y));
        }
    }
    public Rectangle GetRoom()
    {
        if (room != null)
            return room;
        else
        {
            Rectangle leftRoom = null;
            Rectangle rightRoom = null;
            if (leftChild != null)
                leftRoom = leftChild.GetRoom();
            if (rightChild != null)
                rightRoom = rightChild.GetRoom();

            if (leftRoom == null && rightRoom == null)
                return null;
            else if (rightRoom == null)
                return leftRoom;
            else if (leftRoom == null)
                return rightRoom;
            else if (_rand.NextSingle() > 0.5)
                return leftRoom;
            else
                return rightRoom;
        }
    }

    public void CreateHall(Rectangle left, Rectangle right)
    {
        halls = new List<Rectangle>();
        
        Vector point1 = new Vector(_rand.Next(left.Left + 1, left.Right - 2), _rand.Next(left.Top + 1, left.Bottom - 2));
        Vector point2 = new Vector(_rand.Next(right.Left + 1, right.Right - 2), _rand.Next(right.Top + 1, right.Bottom - 2));
        
        int size = 2;
        double w = point2.X - point1.X;
        double h = point2.Y - point1.Y;

        if (w < 0)
        {
            if (h < 0)
            {
                if (_rand.NextSingle() < 0.5)
                {
                    halls.Add(new Rectangle(new Vector(point2.X, point1.Y), new Vector((int)Math.Abs(w), size)));
                    halls.Add(new Rectangle(new Vector(point2.X, point2.Y), new Vector(size, (int)Math.Abs(h))));
                }
                else
                {
                    halls.Add(new Rectangle(new Vector(point2.X, point2.Y), new Vector((int)Math.Abs(w), 1)));
                    halls.Add(new Rectangle(new Vector(point1.X, point2.Y), new Vector( size, (int)Math.Abs(h))));
                }
            }
            else if (h > 0)
            {
                if (_rand.NextSingle() < 0.5)
                {
                    halls.Add(new Rectangle(new Vector(point2.X, point1.Y), new Vector((int)Math.Abs(w), size)));
                    halls.Add(new Rectangle(new Vector(point2.X, point1.Y), new Vector(size, (int)Math.Abs(h))));
                }
                else
                {
                    halls.Add(new Rectangle(new Vector(point2.X, point2.Y), new Vector((int)Math.Abs(w), size)));
                    halls.Add(new Rectangle(new Vector(point1.X, point1.Y), new Vector(size, (int)Math.Abs(h))));
                }
            }
            else
            {
                halls.Add(new Rectangle(new Vector(point2.X, point2.Y), new Vector((int)Math.Abs(w), size)));
            }
        }
        else if (w > 0)
        {
            if (h < 0)
            {
                if (_rand.NextSingle() < 0.5)
                {
                    halls.Add(new Rectangle(new Vector(point1.X, point2.Y), new Vector((int)Math.Abs(w), size)));
                    halls.Add(new Rectangle(new Vector(point2.X, point2.Y), new Vector(size, (int)Math.Abs(h))));
                }
                else
                {
                    halls.Add(new Rectangle(new Vector(point1.X, point1.Y), new Vector((int)Math.Abs(w), size)));
                    halls.Add(new Rectangle(new Vector(point2.X, point2.Y), new Vector(size, (int)Math.Abs(h))));
                }
            }
            else if (h > 0)
            {
                if (_rand.NextSingle() < 0.5)
                {
                    halls.Add(new Rectangle(new Vector(point1.X, point1.Y), new Vector((int)Math.Abs(w), size)));
                    halls.Add(new Rectangle(new Vector(point2.X, point1.Y), new Vector(size, (int)Math.Abs(h))));
                }
                else
                {
                    halls.Add(new Rectangle(new Vector(point1.X, point2.Y), new Vector((int)Math.Abs(w), size)));
                    halls.Add(new Rectangle(new Vector(point1.X, point1.Y), new Vector(size, (int)Math.Abs(h))));
                }
            }
            else
            {
                halls.Add(new Rectangle(new Vector(point1.X, point1.Y), new Vector((int)Math.Abs(w), size)));
            }
        }
        else
        {
            if (h < 0)
            {
                halls.Add(new Rectangle(new Vector(point2.X, point2.Y), new Vector(size,(int)Math.Abs(h))));
            }
            else if (h > 0)
            {
                halls.Add(new Rectangle(new Vector(point1.X, point1.Y),new Vector(size, (int)Math.Abs(h))));
            }
        }
    }
}