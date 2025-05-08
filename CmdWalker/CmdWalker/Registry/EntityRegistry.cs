namespace CmdWalker;

internal class EntityRegistry
{
    public static readonly List<Type> UnitTypes = new List<Type>()
    {
        typeof(Player),
        typeof(Skeleton),
    };
    public static readonly List<Type> ItemTypes = new List<Type>()
    {
        typeof(BounceBullet),
        typeof(Bullet),
        typeof(Gun),
    };
    public static readonly List<Type> EntityTypes = new List<Type>()
    {
      typeof(Portal)  
    };
}