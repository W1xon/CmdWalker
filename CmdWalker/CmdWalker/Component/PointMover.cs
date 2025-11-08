namespace CmdWalker;

internal class PointMover
{
        private List<Vector> _waypoints;
        private SearchPath _pathFinder;
        private Vector _target;
        private GameEntity _parent;
        private Random _random = new Random();
        private Map _map;

        int _maxIteration;
        private Vector _oldPosition;
        private int _stuckCount;
        public PointMover(GameEntity parent)
        {
            _parent = parent;
            _pathFinder = new SearchPath(_parent.Transform.Size);
            _waypoints = new List<Vector>();
        }
        public Vector GetDirection(Map map)
        {
            Init(map);
            var player = map.EntityManager.GetEntity<Player>().First();
            
            if(player == null) 
                return Vector.zero;
            if (Vector.Distance(player.Transform.Position, _parent.Transform.Position) < 20)
            {
                _target = _pathFinder.GetNextPosition(map.Plane, _parent.Transform.Position, player.Transform.Position, _maxIteration) != Vector.zero 
                    ? player.Transform.Position : _waypoints.GetRandomValue();
            }
            else
            {
                CheckingDestinationPoint();
            }
            CheckOldPosition();
            return _pathFinder.GetNextPosition(map.Plane, _parent.Transform.Position, _target, _maxIteration) - _parent.Transform.Position;
        }

        private void Init(Map map)
        {
            if (_map == null)
            {
                _map = map;
            }

            if (_waypoints != null && _waypoints.Count > 0) return;
             _maxIteration = (int)(map.Size.X * map.Size.Y * 0.25f);
            GenerateWaypoints();
            _target = _waypoints.GetRandomValue();
        }

        private void CheckingDestinationPoint()
        {
            if(!_waypoints.Contains(_target) || Vector.Distance(_target,_parent.Transform.Position ) <= 1 || _stuckCount > 3)
                _target = _waypoints.GetRandomValue();
        }
        private void CheckOldPosition()
        {
            if (_oldPosition == _parent.Transform.Position)
                _stuckCount++;
            else
                _stuckCount = 0;
            _oldPosition = _parent.Transform.Position;
        }
        private void GenerateWaypoints()
        {
            int totalPoint = _random.Next(2, 5);
            Vector areaSize = _parent.Transform.Size;
            int radius = _random.Next(15, 25);
            for (int i = 0; i < totalPoint; i++)
            {
                List<Vector> reachableCells =  ReachableZoneScanner.GetReachableCells(_map.Carcas, _parent.Transform.Position, radius);
                Vector point;
                do
                {
                     point = reachableCells.GetRandomValueAndRemove();
                } while ( !_map.Carcas.IsFree(point, areaSize));
                _waypoints.Add(point);
            }
        }
}