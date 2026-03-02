using UnityEngine;

public class AttackState : UnitBaseState
{
    public AttackState(Unit owner, UnitMover mover, UnitCombat combat) : base(owner, mover, combat)
    { }

    public override void Enter()
    {
        Mover.Stop();
    }

    public override void Update()
    {
        var target = Owner.CurrentTarget;
        if (target == null || target.IsDead)
        {
            Owner.ChangeState(UnitStateType.Chase);
            return;
        }

        var dist = Vector3.Distance(Owner.transform.position, target.transform.position);

        if (!Combat.IsInRange(dist))
        {
            Owner.ChangeState(UnitStateType.Chase);
            return;
        }

        Mover.RotateTo(target.transform.position);
        var currentDamage = Owner.GetAttackDamage(); 
        Combat.TryExecuteAttack(Owner, target, currentDamage );
    }
}