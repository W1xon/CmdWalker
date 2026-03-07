namespace CmdWalker;

internal class PointMover
{
    private readonly GameEntity _parent;
    private readonly SearchPath _pathFinder;
    private Vector _patrolTarget;

    private const float AggroRange = 20f;
    private const int ArrivalThreshold = 1;

    private Vector CurrentPos => _parent.Transform.Position;
    private Vector EntitySize => _parent.Transform.Size;

    public PointMover(GameEntity parent)
    {
        _parent = parent;
        _pathFinder = new SearchPath();
    }

    public Vector GetDirection(Map map)
    {
        var player = map.EntityManager.GetPlayer();
        if (player == null) return Vector.Zero;

        var playerPos = player.Transform.Position;
        
        if (Vector.Distance(CurrentPos, playerPos) > AggroRange) 
            return PatrolLogic(map);

        var nextStep = GetNextStepTo(map, playerPos);
        
        if (nextStep == CurrentPos || nextStep == Vector.Zero)
            return PatrolLogic(map);

        return CurrentPos.DirectionTo(nextStep);
    }

    private Vector PatrolLogic(Map map)
    {
        if (_patrolTarget == Vector.Zero || Vector.Distance(CurrentPos, _patrolTarget) <= ArrivalThreshold)
        {
            _patrolTarget = map.GetFreePosition(EntitySize);
        }

        var nextStep = GetNextStepTo(map, _patrolTarget);

        if (nextStep == Vector.Zero)
        {
            _patrolTarget = Vector.Zero;
            return Vector.Zero;
        }

        return nextStep != CurrentPos ? CurrentPos.DirectionTo(nextStep) : Vector.Zero;
    }

    private Vector GetNextStepTo(Map map, Vector target) 
    {
        return _pathFinder.GetNextPosition(map, CurrentPos, target, EntitySize);
    }
}