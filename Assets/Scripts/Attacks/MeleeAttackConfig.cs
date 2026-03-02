using UnityEngine;

[CreateAssetMenu(fileName = "MeleeAttack", menuName = "ArmyClash/Attacks/Melee Attack")]
public class MeleeAttackConfig : UnitAttackConfig
{
    public override bool ExecuteAttack(Unit attacker, Unit target, float baseDamage)
    {
        if (target == null || target.IsDead)
            return false;

        var finalDamage = baseDamage * DamageMultiplier;
        target.TakeDamage(finalDamage);

        return true;
    }
}