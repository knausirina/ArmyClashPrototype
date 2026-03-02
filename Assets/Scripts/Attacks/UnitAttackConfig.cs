using UnityEngine;

public abstract class UnitAttackConfig : ScriptableObject
{
    [field: SerializeField] public float AttackRange { get; protected set; } = 1f;
    [field: SerializeField] public float DamageMultiplier { get; protected set; } = 1f;

    public abstract bool ExecuteAttack(Unit attacker, Unit target, float baseDamage);
}