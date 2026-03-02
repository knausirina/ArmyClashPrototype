public abstract class UnitBaseState : IUnitState
{
    protected readonly Unit Owner;
    protected readonly UnitMover Mover;
    protected readonly UnitCombat Combat;

    protected UnitBaseState(Unit owner, UnitMover mover, UnitCombat combat)
    {
        Owner = owner;
        Mover = mover;
        Combat = combat;
    }

    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void Exit() { }
}