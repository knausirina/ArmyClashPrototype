using System.Collections.Generic;
using UnityEngine;

public class BaseParameterConfig : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [SerializeField] private List<StatModifier> _modifiers;

    public void ApplyStatsModifiers(UnitStats stats)
    {
        if (_modifiers == null || stats == null) return;

        foreach (var modifier in _modifiers)
        {
            stats.AddModifier(modifier); 
        }
    }
}