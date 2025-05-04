namespace CmdWalker;
internal abstract class TemplateBuilder
{
    
    protected MapTemplate _template = new MapTemplate();
    public abstract  void AddEntities();
    public abstract void AddItems();
    public abstract void AddUnits();
    public abstract void SetConfig();
    public MapTemplate GetTemplate() => _template;
}