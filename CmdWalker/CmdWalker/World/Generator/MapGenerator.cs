namespace CmdWalker
{
    internal class MapGenerator(IMapBuilder builder)
    {
        public Map Generate(ContentBuilder contentBuilder)
        {
            var content = GenerateContentCarcass(contentBuilder);
            builder.AddCarcass(content.CarcassGenerator);
            contentBuilder.SetTileMap(builder.GetMap().Carcas);
            content = GenerateContent(contentBuilder);
            builder.AddItem(content.Items);
            builder.AddEntity(content.GameEntities);
            builder.AddUnit(content.Units);
            builder.GetMap().InitializePlane();
            Console.SetCursorPosition(0, 0);
            return builder.GetMap();
        }

        private MapContent GenerateContentCarcass(ContentBuilder contentBuilder)
        {
            contentBuilder.SetConfig();
            contentBuilder.AddCarcassBuilder();

            return contentBuilder.GetTemplate();
        }

        private MapContent GenerateContent(ContentBuilder contentBuilder)
        {
            contentBuilder.AddEntities();
            contentBuilder.AddItems();
            contentBuilder.AddUnits();
            return contentBuilder.GetTemplate();
        }
    }
}
