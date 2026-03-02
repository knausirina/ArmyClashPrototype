using UnityEngine;

public abstract class FormationConfig : ScriptableObject
{
    [field: SerializeField] public float UnitSpacing { get; protected set; } = 1.5f;
    [field: SerializeField] public int SpawnColumns { get; protected set; } = 5;

    public abstract IFormationStrategy CreateStrategy();
}