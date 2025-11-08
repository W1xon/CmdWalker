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
        
        public void AddCarcass(CarcassGenerator carcassGenerator)
        {
            _map.Carcas.SetTile(carcassGenerator.Generate());
            _map.Carcas.CopyTo(_map.Plane);
        }
        public void AddEntity(List<GameEntity> entities)
        {
            if (entities == null) return;
            foreach(var entity in entities)
            {
                _map.EntityManager.SpawnEntity(entity);
            }
        }
        public void AddItem(List<GameEntity> items)
        { 
            if (items == null) return;
            foreach(var item in items)
            {
                _map.EntityManager.SpawnEntity(item);
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
                _map.EntityManager.SpawnEntity(unit);
            }
        }

        public Map GetMap()
        {
            return _map;
        }
    }
}
