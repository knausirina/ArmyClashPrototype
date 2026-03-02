using System.Collections.Generic;

public class UnitStats
{
    private readonly Dictionary<StatType, StatValue> _stats = new ();
    
    public void InitStat(StatType type, float value)
    {
        _stats[type] = new StatValue(value);
    }

    public void AddModifier(StatModifier modifier)
    {
        if (modifier == null || modifier.StatType == null)
            return;

        if (_stats.TryGetValue(modifier.StatType, out var statValue))
        {
            statValue.Modifiers.Add(modifier);
            statValue.IsDirty = true;
        }
    }

    public void RemoveModifier(StatModifier mod)
    {
        if (_stats.TryGetValue(mod.StatType, out var stat))
        {
            if (stat.Modifiers.Remove(mod))
                stat.IsDirty = true;
        }
    }

    public float GetStat(StatType type)
    {
        if (!_stats.TryGetValue(type, out var stat))
            return 0;

        if (stat.IsDirty)
        {
            stat.FinalValue = CalculateValue(stat);
            stat.IsDirty = false;
        }
        return stat.FinalValue;
    }

    private float CalculateValue(StatValue stat)
    {
        var finalValue = stat.BaseValue;
        float sumPercent = 0;

        foreach (var mod in stat.Modifiers)
        {
            if (mod.Type == StatModType.Flat)
            {
                finalValue += mod.Value;
            }
            else if (mod.Type == StatModType.Percent)
            {
                sumPercent += mod.Value;
            }
        }

        finalValue *= (1 + sumPercent);

        return finalValue;
    }
}