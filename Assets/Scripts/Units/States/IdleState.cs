using UnityEngine;

public class IdleState : UnitBaseState
{
    private readonly ITargetingService _targeting;
    private const float SearchInterval = 0.2f;
    private float _searchTimer;

    public IdleState(Unit owner, UnitMover mover, UnitCombat combat, ITargetingService targeting) 
        : base(owner, mover, combat)
    {
        _targeting = targeting;
    }

    public override void Enter()
    {
        Mover.Stop();
        
        _searchTimer = Random.Range(0, SearchInterval);
    }

    public override void Update()
    {
        if (Owner.CurrentMode != UnitMode.Battle)
            return;

        _searchTimer -= Time.deltaTime;
        if (_searchTimer <= 0)
        {
            _searchTimer = SearchInterval;
            
            var target = _targeting.FindEnemy(Owner);
            if (target != null)
            {
                Owner.SetTarget(target);
                Owner.ChangeState(UnitStateType.Chase);
            }
        }
    }
}