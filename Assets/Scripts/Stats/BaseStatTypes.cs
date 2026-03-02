using UnityEngine;

[CreateAssetMenu(fileName = "BaseStatTypes", menuName = "ArmyClash/Stats/Base Stat Types")]
public class BaseStatTypes : ScriptableObject
{
    [field: SerializeField] public StatType Hp { get; private set; }
    [field: SerializeField] public StatType Atk { get; private set; }
    [field: SerializeField] public StatType Speed { get; private set; }
    [field: SerializeField] public StatType AtkSpd { get; private set; }

    [field: SerializeField] public StatType[] AllStats { get; private set; }

    private void OnValidate()
    {
        System.Collections.Generic.List<StatType> stats = new System.Collections.Generic.List<StatType>();

        if (Hp != null) stats.Add(Hp);
        if (Atk != null) stats.Add(Atk);
        if (Speed != null) stats.Add(Speed);
        if (AtkSpd != null) stats.Add(AtkSpd);

        AllStats = stats.ToArray();
    }
}