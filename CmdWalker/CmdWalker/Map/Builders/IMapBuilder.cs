namespace CmdWalker
{
    internal interface IMapBuilder
    {
        public abstract Map GetMap();
        public abstract void AddBorder(Vector size);
        public abstract void AddRoom(List<Room> rooms);
        public abstract void AddEntity(List<GameEntity> entities);
        public abstract void AddUnit(List<Unit> units);
    }
}
