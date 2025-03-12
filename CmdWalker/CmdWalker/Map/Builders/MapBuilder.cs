using System.Numerics;

namespace CmdWalker
{
    internal class MapBuilder : IMapBuilder
    {
        protected Map _map;
        protected Random _rand = new Random();
        public MapBuilder()
        {
            _map = new Map();
        }

        public void AddBorder(Vector size)
        {
            _map.Carcas = new char[size.Y][];
            //Базовая генерация стен
            for (int y = 0; y < size.Y; y++)
            {
                _map.Carcas[y] = new char[size.X];
                for (int x = 0; x < size.X; x++)
                {
                    if (x == 0 || x == size.X - 1 || y == 0 || y == size.Y - 1)
                    {
                        _map.Carcas[y][x] = Blocks.GetGlyph(Block.Wall);
                    }
                    else
                    {
                        _map.Carcas[y][x] = Blocks.GetGlyph(Block.Floor);
                    }
                }
            }
            _map.Plane = _map.Carcas.DeepCopy();
        }
        public void AddEntity(List<GameEntity> entities)
        {
            if (entities == null) return;
            foreach(var entity in entities)
            {
                _map.SpawnEntity(entity);
            }
        }

        public void AddRoom(List<Room> rooms)
        {
            if (rooms == null) return;
            foreach (var room in rooms)
            {
                _map.BuildStructure(room);
            }
        }


        public void AddUnit(List<Unit> units)
        {
            if (units == null) return;
            foreach (var unit in units)
            {
                _map.SpawnEntity(unit);
            }
        }

        public Map GetMap()
        {
            return _map;
        }
    }
}
