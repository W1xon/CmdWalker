namespace CmdWalker
{
    internal class Game
    {
        public static Map map;
        public MapTemplate CreateTemplate()
        {
            var mapTemplate = new MapTemplate();
            mapTemplate.MaxRoomSize = 20;
            mapTemplate.Size = new Vector(80,30);
            mapTemplate.Units = new List<Unit>()
            {
                (Player)(new PlayerCreator().Create(new Vector(3, 5))),
                (Skillet)(new SkilletCreator().Create(new Vector(60, 5))),
            };
            BulletCreator bulletCreator = new BulletCreator();
            mapTemplate.Items = new List<GameEntity>()
            {
                bulletCreator.CreateOnMap(new Vector(10,10)),
            };
            return mapTemplate;
        }
        public void Start()
        {
            
            MapBuilder mapBuilder = new MapBuilder();
            MapGenerator mapGenerator = new MapGenerator(mapBuilder);
            var template = CreateTemplate();
            map = mapGenerator.Generate(template, new BSPCarcasGenerator(template));
            map.Show();

            ConsoleDebugView.SetMap(map);
            Update();
        }

        private void Update()
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
