namespace CmdWalker
{
    internal class Game
    {
        public static Map map;
        public MapTemplate CreateTemplate()
        {
            var mapTemplate = new MapTemplate();
            mapTemplate.Size = new Vector(80,30);
            mapTemplate.Rooms = new List<Room>()
            {
                new Room(new Vector(15,5), new Vector(10,5)),
                new Room(new Vector(35,5), new Vector(5,5)),
            };
            mapTemplate.Units = new List<Unit>()
            {
                (Player)(new PlayerCreator().Create(new Vector(3, 5))),
                (Skillet)(new SkilletCreator().Create(new Vector(10, 5))),
            };
            return mapTemplate;
        }
        public void Start()
        {
            MapBuilder mapBuilder = new MapBuilder();
            MapGenerator mapGenerator = new MapGenerator(mapBuilder);
            map = mapGenerator.Generate(CreateTemplate());
            map.Show();

            ConsoleDebugView.SetMap(map);
            Update();
        }
        public void Update()
        {
            while (true)
            {
                Thread.Sleep(10);
                InputHandler.UpdateInput();
                ConsoleDebugView.Show();
                foreach (var entity in map.Entities)
                {
                    entity.Update();
                }
            }
        }
    }
}
