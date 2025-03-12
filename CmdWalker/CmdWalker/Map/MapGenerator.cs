
namespace CmdWalker
{
    internal class MapGenerator
    {
        private IMapBuilder _builder;
        public MapGenerator(IMapBuilder builder)
        {
            _builder = builder;
        }
        public Map Generate(MapTemplate template)
        {
            _builder.AddBorder(template.Size);
            _builder.AddRoom(template.Rooms);
            _builder.AddEntity(template.GameEntities);
            _builder.AddUnit(template.Units);
            _builder.GetMap().InitializePlane();
            Console.SetCursorPosition(0, 0);
            return _builder.GetMap();
        }
    }
}
