namespace CmdWalker;

internal class SafeZoneTemplateBuilder : TemplateBuilder
{
    public override void AddEntities()
    {
        _template.GameEntities = new List<GameEntity>()
        {
            CreatorRegistry.GetCreator<PortalCreator, Portal>().Create(new Vector(0, 7), true),
        };
    }

    public override void AddItems()
    {
        //достаем инфу из сохранений в дальнейшем
    }

    public override void AddUnits()
    {
        _template.Units = new List<Unit>()
        {
            (Player)CreatorRegistry.GetCreator<PlayerCreator, Player>().Create(new Vector(15,7)),
        };
    }

    public override void SetConfig()
    {
        _template.Size = new Vector(40, 15);
    }
}