using UnityEngine;

[CreateAssetMenu(fileName = "UnitConfig", menuName = "ArmyClash/Unit Config")]
public class UnitConfig : ScriptableObject
{
    [field: SerializeField] public float DefaultHp { get; private set; } = 100f;
    [field: SerializeField] public float DefaultAtk { get; private set; } = 10f;
    [field: SerializeField] public float DefaultSpeed { get; private set; } = 10f;
    [field: SerializeField] public float DefaultAtkSpd { get; private set; } = 1f;

    [field: SerializeField] public BaseStatTypes BaseStatTypes { get; private set; }
    [field: SerializeField] public UnitShapeConfig[] Shapes{ get; private set; }
    [field: SerializeField] public UnitSizeConfig[] Sizes{ get; private set; }
    [field: SerializeField] public UnitColorConfig[] Colors{ get; private set; }
    [field: SerializeField] public UnitAttackConfig[] Attacks { get; private set; }
    [field: SerializeField] public UnitAttackConfig DefaultAttack { get; private set; }

    public UnitStats CalculateStats(UnitShapeConfig shape, UnitSizeConfig size, UnitColorConfig color)
    {
        UnitStats stats = new UnitStats();

        stats.InitStat(BaseStatTypes.Hp, DefaultHp);
        stats.InitStat(BaseStatTypes.Atk, DefaultAtk);
        stats.InitStat(BaseStatTypes.Speed, DefaultSpeed);
        stats.InitStat(BaseStatTypes.AtkSpd, DefaultAtkSpd);

        if (shape != null)
            shape.ApplyStatsModifiers(stats);
        if (size != null)
            size.ApplyStatsModifiers(stats);
        if (color != null)
            color.ApplyStatsModifiers(stats);

        return stats;
    }

    public UnitShapeConfig GetRandomShape()
    {
        return GetRandom(Shapes, null);
    }

    public UnitSizeConfig GetRandomSize()
    {
        return GetRandom(Sizes, null);
    }

    public UnitColorConfig GetRandomColor()
    {
        return GetRandom(Colors, null);
    }

    public UnitAttackConfig GetRandomAttack()
    {
        return GetRandom(Attacks, DefaultAttack);
    }

    private T GetRandom<T>(T[] array, T defaultValue)
    {
        if (array == null || array.Length == 0)
            return defaultValue;

        return array[Random.Range(0, array.Length)];
    }
}