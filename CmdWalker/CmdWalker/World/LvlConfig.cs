namespace CmdWalker;
internal enum LvlDifficult
{ 
    Easy,
    Medium,
    Hard,
    Extreme,
}
internal class LvlConfig
{
    public Vector Size;
    public int MaxRoomSize;
    public int RoomCount;
    public LvlDifficult Difficult;
    public Dictionary<Type, int> UnitPreferences = new Dictionary<Type,  int>();
    public Dictionary<Type, int> ItemPreferences = new Dictionary<Type,  int>();
    public Dictionary<Type, int> EntityPreferences = new Dictionary<Type,  int>();
    private Random _random = new Random();
    public LvlConfig(LvlDifficult difficult)
    {
        Difficult = difficult;
    }

    public void Configure()
    {
        CreateEntities(UnitPreferences, 0.25f);
        CreateEntities(ItemPreferences, 0.2f);
        CreateEntities(EntityPreferences, 0.01f);
    }
    private void CreateEntities(Dictionary<Type, int> preferences, float seed)
    {
        int max = CalculateMaxEntities(seed);
        int count = 0;

        while (count <= max)
        {
            if (max < 1) break;
            int num = _random.Next(1, 1);
            Type entityType = null;

            if (preferences == UnitPreferences)
                entityType = EntityRegistry.UnitTypes.GetRandomValue();
            else if (preferences == ItemPreferences)
                entityType = EntityRegistry.ItemTypes.GetRandomValue();
            else if (preferences == EntityPreferences)
                entityType = EntityRegistry.EntityTypes.GetRandomValue();
            
            if (entityType == typeof(Player))
                continue;

            preferences.TryGetValue(entityType, out int current);
            preferences[entityType] = current + num;
            count += num;
        }

        if (!EntityPreferences.ContainsKey(typeof(Portal)))
            EntityPreferences.Add(typeof(Portal), 1);
    }


    private int CalculateMaxEntities(float seed)
    {
        int totalTiles = Size.X * Size.Y;
        double difficultyModifier = 0;
        if (Difficult == LvlDifficult.Easy)
        {
            difficultyModifier = _random.NextDouble(0.01, 0.03);
        }
        else if (Difficult == LvlDifficult.Medium)
        {
            difficultyModifier = _random.NextDouble(0.05, 0.06);
        }
        else if (Difficult == LvlDifficult.Hard)
        {
            difficultyModifier = _random.NextDouble(0.075, 0.1);
        }
        else if (Difficult == LvlDifficult.Extreme)
        {
            difficultyModifier = _random.NextDouble(0.1, 0.15);
        }
        return (int)Math.Ceiling(totalTiles * difficultyModifier * Math.Pow(seed, 2));
    }
}