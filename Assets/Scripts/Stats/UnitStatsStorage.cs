using System.Collections.Generic;

public class UnitStatsStorage
{
    private readonly Dictionary<Unit, UnitStats> _storage = new();

    public void Register(Unit unit, UnitStats stats)
    {
        _storage[unit] = stats;
    }

    public void Unregister(Unit unit)
    {
        _storage.Remove(unit);
    }

    public void Clear()
    {
        _storage.Clear();
    }

    public UnitStats GetStats(Unit unit)
    {
        _storage.TryGetValue(unit, out var stats);
        return stats;
    }

    public float GetStat(Unit unit, StatType type)
    {
        if (_storage.TryGetValue(unit, out var stats))
        {
            return stats.GetStat(type);
        }
        return 0;
    }
}
