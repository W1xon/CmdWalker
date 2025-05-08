namespace CmdWalker
{
    internal interface IMapBuilder
    {
        public Map GetMap();
        public void AddCarcass(CarcassGenerator carcassGenerator);
        public void AddItem(List<GameEntity> items);
        public void AddRoom(List<Room> rooms);
        public void AddEntity(List<GameEntity> entities);
        public void AddUnit(List<Unit> units);
    }
}
