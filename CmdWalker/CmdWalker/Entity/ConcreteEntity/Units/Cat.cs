namespace CmdWalker;

internal class Cat : Unit
{
    private List<char[,]> _sprites =
    [
        new char[,]
        {
            { ' ', '/', '\\', '_', '/', '\\', ' ' },
            { '(', ' ', 'o', '.', 'o', ' ', ')' },
            { ' ', '>', ' ', '^', ' ', '<', ' ' }
        },

        new char[,]
        {
            { ' ', '/', '\\', '_', '/', '\\', ' ' },
            { '(', ' ', '-', '.', '-', ' ', ')' },
            { ' ', '>', ' ', '^', ' ', '<', ' ' }
        },

        new char[,]
        {
            { ' ', '/', '\\', '_', '/', '\\', ' ' },
            { '(', ' ', 'o', '.', 'o', ' ', ')' },
            { ' ', '>', ' ', 'v', ' ', '<', ' ' }
        }
    ];
    
    private int _tickCounter = 0;
    private readonly int _ticksBeforeChangeSprite = Game.TargetFPS;
    private byte _currentFrame = 0;
    public Cat(Vector position) : base(position)
    {
        _health = new Health(100);
        Visual = new Sprite(_sprites[0], ConsoleColor.Green);
    }
    public override void Update()
    {
        _map.SetCells(Transform.Position, Visual);
        if (_tickCounter++ < _ticksBeforeChangeSprite)
            return;
        
        _tickCounter = 0;
        if (++_currentFrame >= _sprites.Count) _currentFrame = 0;
        Visual.Representation = _sprites[_currentFrame];
    }

    public override void Destroy()
    {
        throw new NotImplementedException();
    }

    public override void Move(Vector direction)
    {
        throw new NotImplementedException();
    }

    public override bool CanMoveDir(Vector dir)
    {
        throw new NotImplementedException();
    }
}