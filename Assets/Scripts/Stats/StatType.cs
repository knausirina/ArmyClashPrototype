using UnityEngine;

[CreateAssetMenu(fileName = "StatType", menuName = "ArmyClash/Stats/Stat Type")]
public class StatType : ScriptableObject
{
    [field: SerializeField] public string Id { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public string DisplayName { get; private set; }

    [field: SerializeField] public float MinValue { get; private set; } = float.MinValue;
    [field: SerializeField] public float MaxValue { get; private set; } = float.MaxValue;
}