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
                (Player)CreatorRegistry.GetCreator<PlayerCreator>(typeof(Player)).Create(new Vector(3, 5)),
                (Skeleton)(CreatorRegistry.GetCreator<SkilletCreator>(typeof(Skeleton)).Create(new Vector(60, 5))),
                (Skeleton)(CreatorRegistry.GetCreator<SkilletCreator>(typeof(Skeleton)).Create(new Vector(70, 15))),
            };
            mapTemplate.Items = new List<GameEntity>()
            {
                CreatorRegistry.GetCreator<BulletCreator, Bullet>().CreateOnMap(new Vector(10,10)),
                CreatorRegistry.GetCreator<BulletCreator, Bullet>().CreateOnMap(new Vector(20,19)),
                CreatorRegistry.GetCreator<BulletCreator, Bullet>().CreateOnMap(new Vector(39,1)),
                CreatorRegistry.GetCreator<BulletCreator, Bullet>().CreateOnMap(new Vector(40,1)),
                CreatorRegistry.GetCreator<BulletCreator, Bullet>().CreateOnMap(new Vector(41,1)),
                CreatorRegistry.GetCreator<BulletCreator, Bullet>().CreateOnMap(new Vector(70,6)),
                CreatorRegistry.GetCreator<GunCreator, Gun>().CreateOnMap(new Vector(4,6)),
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
