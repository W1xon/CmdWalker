namespace CmdWalker
{
    internal class MapGenerator(IMapBuilder builder)
    {
        public Map Generate(MapTemplate template, CarcassGenerator carcassGenerator)
        {
            builder.AddCarcass(carcassGenerator);
            builder.AddItem(template.Items);
            builder.AddEntity(template.GameEntities);
            builder.AddUnit(template.Units);
            builder.GetMap().InitializePlane();
            Console.SetCursorPosition(0, 0);
            return builder.GetMap();
        }

        public MapTemplate GenerateTemplate(TemplateBuilder templateBuilder)
        {
            templateBuilder.AddEntities();
            templateBuilder.AddItems();
            templateBuilder.AddUnits();
            templateBuilder.SetConfig();

            return templateBuilder.GetTemplate();
        }
    }
}
