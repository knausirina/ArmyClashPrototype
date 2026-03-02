using UnityEngine;

[System.Serializable]
public class StatModifier
{
    [field: SerializeField] public StatType StatType { get; private set; }
    [field:SerializeField] public float Value{ get; private set; }
    [field:SerializeField] public StatModType Type{ get; private set; }

    public StatModifier(StatType type, float value, StatModType modType)
    {
        StatType = type;
        Value = value;
        Type = modType;
    }
}