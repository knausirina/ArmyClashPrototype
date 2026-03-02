public class UnitCombat
{
    private readonly UnitAttackConfig _attackConfig;
    private readonly UnitStatsStorage _statsStorage;
    private readonly StatType _atkSpdType;
    
    private float _cooldown;

    public UnitCombat(UnitAttackConfig config, UnitStatsStorage statsStorage, StatType atkSpdType)
    {
        _attackConfig = config;
        _statsStorage = statsStorage;
        _atkSpdType = atkSpdType;
    }

    public bool IsInRange(float distance)
    {
        return distance <= _attackConfig.AttackRange;
    }

    public void Tick(float deltaTime)
    {
        if (_cooldown > 0)
        {
            _cooldown -= deltaTime;
        }
    }

    public void TryExecuteAttack(Unit attacker, Unit target, float damage)
    {
        if (_cooldown > 0)
            return;

        var success = _attackConfig.ExecuteAttack(attacker, target, damage);
        if (success)
        {
            _cooldown = _statsStorage.GetStat(attacker, _atkSpdType);
        }
    }
}