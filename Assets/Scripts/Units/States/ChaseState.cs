using UnityEngine;

public class ChaseState : UnitBaseState
{
    private readonly ITargetingService _targeting;
    private float _repathTimer;

    private const float TimeUpdate = 0.2f;

    public ChaseState(Unit owner, UnitMover mover, UnitCombat combat, ITargetingService targeting)
        : base(owner, mover, combat)
    {
        _targeting = targeting;
    }

    public override void Update()
    {
        var target = Owner.CurrentTarget;

        if (target == null || target.IsDead)
        {
            var newTarget = _targeting.FindEnemy(Owner);
            Owner.SetTarget(newTarget);
            if (newTarget == null)
            {
                Mover.Stop();
                Owner.ChangeState(UnitStateType.Idle);
            }
            return;
        }

        var dist = Vector3.Distance(Owner.transform.position, target.transform.position);
        
        if (Combat.IsInRange(dist))
        {
            Owner.ChangeState(UnitStateType.Attack);
            return;
        }

        _repathTimer += Time.deltaTime;
        if (_repathTimer >= TimeUpdate)
        {
            Mover.MoveTo(target.transform.position);
            _repathTimer = 0;
        }
    }
}
