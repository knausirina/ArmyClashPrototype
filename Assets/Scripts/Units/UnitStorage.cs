using System;
using System.Collections.Generic;

public class UnitStorage
{
    public event Action<Team> OnTeamEmpty;
    
    private readonly Dictionary<Team, List<Unit>> _armies = new();

    public UnitStorage()
    {
        foreach (Team team in Enum.GetValues(typeof(Team)))
        {
            _armies[team] = new List<Unit>();
        }
    }

    public void Add(Unit unit)
    {
        _armies[unit.Team].Add(unit);
    }

    public void Remove(Unit unit)
    {
        if (unit == null) 
            return;
        
        var team = unit.Team;
        if (_armies[team].Remove(unit))
        {
            if (_armies[team].Count == 0)
            {
                OnTeamEmpty?.Invoke(team);
            }
        }
    }

    public void ClearAll()
    {
        foreach (Team team in Enum.GetValues(typeof(Team)))
        {
            _armies[team].Clear();
        }
    }

    public IReadOnlyList<Unit> GetTeam(Team team)
    {
        return _armies[team];
    }
}