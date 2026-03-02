using System.Collections.Generic;

public class StatValue
{
    public float BaseValue { get; }
    public float FinalValue { get; set; }
    public bool IsDirty = true;
    public readonly List<StatModifier> Modifiers = new();

    public StatValue(float baseVal)
    {
        BaseValue = baseVal;
    }
}