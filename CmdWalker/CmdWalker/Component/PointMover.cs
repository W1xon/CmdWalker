using System;
using System.Collections.Generic;
using System.Linq;

namespace CmdWalker;

internal class PointMover
{
    private GameEntity _parent;
    private SearchPath _pathFinder;
        
    private Vector _patrolTarget;
    private Random _rnd = new Random();

    public PointMover(GameEntity parent)
    {
        _parent = parent;
        _pathFinder = new SearchPath();
    }

    public Vector GetDirection(Map map)
    {
        var player = map.EntityManager.GetEntity<Player>().FirstOrDefault();
        if (player == null) return Vector.Zero;

        Vector myPos = _parent.Transform.Position;
        Vector targetPos = player.Transform.Position;
            
        float distToPlayer = Vector.Distance(myPos, targetPos);

        if (distToPlayer < 20) 
        {
            if (distToPlayer <= 2)
            {
                return GetDirectDirection(myPos, targetPos);
            }

            Vector nextStep = _pathFinder.GetNextPosition(map, myPos, targetPos, 500);
                
            if (nextStep != myPos) 
                return nextStep - myPos; 
        }

        return PatrolLogic(map, myPos);
    }

    private Vector PatrolLogic(Map map, Vector myPos)
    {
        if (_patrolTarget == Vector.Zero || Vector.Distance(myPos, _patrolTarget) <= 1)
        {
            GenerateNewPatrolTarget(map, myPos);
        }

        Vector nextStep = _pathFinder.GetNextPosition(map, myPos, _patrolTarget, 200);
            
        if (nextStep != myPos)
            return nextStep - myPos;
            
        return Vector.Zero;
    }

    private void GenerateNewPatrolTarget(Map map, Vector center)
    {
        for (int i = 0; i < 10; i++)
        {
            int rx = _rnd.Next(-10, 10);
            int ry = _rnd.Next(-10, 10);
            Vector p = center + new Vector(rx, ry);

            if (map.Carcas.IsFree(p, Vector.One) && p.X > 0 && p.Y > 0)
            {
                _patrolTarget = p;
                return;
            }
        }
    }

    private Vector GetDirectDirection(Vector from, Vector to)
    {
        int dx = to.X - from.X;
        int dy = to.Y - from.Y;

        if (Math.Abs(dx) > Math.Abs(dy)) return new Vector(Math.Sign(dx), 0);
        if (Math.Abs(dy) > 0) return new Vector(0, Math.Sign(dy));
        return Vector.Zero;
    }
}