using System;
using System.Collections.Generic;
using System.Linq;

namespace CmdWalker;

public enum LvlDifficult
{ 
    Easy,
    Medium,
    Hard,
    Extreme,
}

public class LvlConfig
{
    public Vector Size;
    public Vector MaxRoomSize;
    public Vector MinRoomSize;
    public int RoomCount;
    
    public LvlDifficult Difficult { get; }

    public Dictionary<Type, int> UnitPreferences { get; } = new();
    public Dictionary<Type, int> ItemPreferences { get; } = new();
    public Dictionary<Type, int> EntityPreferences { get; } = new();
    
    public List<Construction> Constructions { get; set; } = new();

    private readonly Random _random = new();

    private static readonly Dictionary<LvlDifficult, double> DifficultyMultipliers = new()
    {
        { LvlDifficult.Easy, 0.015 },
        { LvlDifficult.Medium, 0.04 },
        { LvlDifficult.Hard, 0.07 },
        { LvlDifficult.Extreme, 0.12 }
    };

    public LvlConfig(LvlDifficult difficult)
    {
        Difficult = difficult;
    }

    public void Configure()
    {
        UnitPreferences.Clear();
        ItemPreferences.Clear();
        EntityPreferences.Clear();

        PopulateEntities(UnitPreferences, EntityRegistry.UnitTypes, 0.2f);
        PopulateEntities(ItemPreferences, EntityRegistry.ItemTypes, 0.8f);
        PopulateEntities(EntityPreferences, EntityRegistry.EntityTypes, 0.1f);
        
        EnsurePortal();
    }

    private void PopulateEntities(Dictionary<Type, int> targetDict, List<Type> sourceTypes, float typeDensity)
    {
        if (sourceTypes == null || sourceTypes.Count == 0) return;

        int totalTiles = Size.X * Size.Y;
        double diffMod = DifficultyMultipliers[Difficult];

        int maxCount = (int)Math.Ceiling(totalTiles * diffMod * typeDensity);

        for (int i = 0; i < maxCount; i++)
        {
            Type entityType = sourceTypes.GetRandomValue();

            if (entityType == typeof(Player)) 
                continue;

            targetDict.TryGetValue(entityType, out int current);
            targetDict[entityType] = current + 1;
        }
    }

    private void EnsurePortal()
    {
        if (!EntityPreferences.ContainsKey(typeof(Portal)))
        {
            EntityPreferences.Add(typeof(Portal), 1);
        }
    }

    public double GetCurrentMultiplier() => DifficultyMultipliers[Difficult];
}