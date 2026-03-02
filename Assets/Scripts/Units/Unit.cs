using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class Unit : MonoBehaviour
{
    public Action ChangedParameters;
    public bool IsDead { get; private set; }
    public float CurrentHp => _health?.CurrentHp ?? 0f;
    public Team Team { get; private set; }
    public Unit CurrentTarget { get; private set; }
    public UnitMode CurrentMode { get; private set; } = UnitMode.Idle;

    private UnitHealth _health;
    private UnitMover _mover;
    private UnitCombat _combat;
    
    private IUnitState _currentState;
    private Dictionary<UnitStateType, IUnitState> _states;

    private ITargetingService _targeting;
    private UnitStorage _unitStorage;
    private UnitStatsStorage _statsStorage;
    private UnitConfig _unitConfig;
    private EffectsService _effects;

    [Inject]
    private void Construct(ITargetingService targeting, UnitStorage unitStorage, UnitStatsStorage statsStorage,
        UnitConfig unitConfig, EffectsService effects)
    {
        _targeting = targeting;
        _unitStorage = unitStorage;
        _statsStorage = statsStorage;
        _unitConfig = unitConfig;
        _effects = effects;
    }

    public void Initialize(UnitAttackConfig attackConfig, Team team)
    {
        Team = team;

        var hp = _statsStorage.GetStat(this, _unitConfig.BaseStatTypes.Hp);
        var atk = _statsStorage.GetStat(this, _unitConfig.BaseStatTypes.Atk);
        var atkSpd = _statsStorage.GetStat(this, _unitConfig.BaseStatTypes.AtkSpd);
        var speed = _statsStorage.GetStat(this, _unitConfig.BaseStatTypes.Speed);

        _health = new UnitHealth(hp);
        _mover = new UnitMover(GetComponent<NavMeshAgent>(), transform, speed);
        _mover.Disable();
        _combat = new UnitCombat(
            attackConfig, 
            _statsStorage, 
            _unitConfig.BaseStatTypes.AtkSpd
        );

        _states = new Dictionary<UnitStateType, IUnitState>
        {
            { UnitStateType.Idle, new IdleState(this, _mover, _combat, _targeting) },
            { UnitStateType.Chase, new ChaseState(this, _mover, _combat, _targeting) },
            { UnitStateType.Attack, new AttackState(this, _mover, _combat) }
        };

        _health.OnHealthChanged += OnHealthChanged;
        _health.OnDeath += OnHandleDeath;

        ChangeState(UnitStateType.Idle);
    }

    private void OnHealthChanged(float maxHp, float hp, float damage)
    {
        _effects.ShowDamage(transform.position, damage); 
        
        ChangedParameters?.Invoke();
    }

    public void SetBattleState(UnitMode newMode)
    {
        if (CurrentMode == UnitMode.Dead)
        {
            return;
        }
        
        CurrentMode = newMode;
        
        if (CurrentMode == UnitMode.Battle)
        {
            _mover.Enable();
            ChangeState(UnitStateType.Idle); 
        }
    }

    public void ChangeState(UnitStateType type)
    {
        _currentState?.Exit();
        _currentState = _states[type];
        _currentState.Enter();
    }

    public float GetAttackDamage()
    {
        return _statsStorage.GetStat(this, _unitConfig.BaseStatTypes.Atk);
    }

    private void Update()
    {
        if (CurrentMode != UnitMode.Battle || IsDead) 
            return;
        
        _currentState.Update();
        _combat.Tick(Time.deltaTime);
    }

    public void TakeDamage(float damage)
    {
        _health.TakeDamage(damage);
    }

    public void SetTarget(Unit target)
    {
        CurrentTarget = target;
    }

    private void OnHandleDeath()
    {
        if (IsDead)
            return;
        IsDead = true;
        
        CurrentMode = UnitMode.Dead;
        _currentState?.Exit(); 
        
        _health.OnHealthChanged -= OnHealthChanged;
        _health.OnDeath -= OnHandleDeath;

        _unitStorage.Remove(this);
        _statsStorage.Unregister(this);

        _mover.Disable();
        
        ChangedParameters = null;
        Destroy(gameObject, 0.5f);
    }

    private void OnDestroy()
    {
        _health.OnHealthChanged -= OnHealthChanged;
        _health.OnDeath -= OnHandleDeath;
        
        _unitStorage.Remove(this);
        _statsStorage.Unregister(this);
    }
}
