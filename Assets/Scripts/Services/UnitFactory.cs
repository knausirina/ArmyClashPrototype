using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class UnitFactory : IUnitFactory
{
    private readonly DiContainer _container;
    private readonly UnitConfig _unitConfig;
    private readonly UnitStatsStorage _unitStatsStorage;

    public UnitFactory(DiContainer container, UnitConfig unitConfig, UnitStatsStorage unitStatsStorage)
    {
        _container = container;
        _unitConfig = unitConfig;
        _unitStatsStorage = unitStatsStorage;
    }

    public GameObject CreateUnit(UnitShapeConfig shapeConfig, 
                                UnitSizeConfig sizeConfig, 
                                UnitColorConfig colorConfig, 
                                UnitAttackConfig attackConfig, 
                                Team team, Vector3 position)
    {
        var unitGo = new GameObject($"Unit_{team}");
        unitGo.transform.position = position;

        var visual = shapeConfig.CreateVisual(unitGo.transform);
        sizeConfig.ApplyScale(visual.transform);
        visual.transform.localPosition = new Vector3(0, sizeConfig.SizeScale / 2f, 0);
    
        var renderer = visual.GetComponent<Renderer>();
        if (renderer != null)
            colorConfig.ApplyMaterial(renderer);

        var stats = _unitConfig.CalculateStats(shapeConfig, sizeConfig, colorConfig);
        var unit = unitGo.AddComponent<Unit>();
        _unitStatsStorage.Register(unit, stats); 
        _container.Inject(unit);
        
        var agent = unitGo.AddComponent<NavMeshAgent>();
        agent.speed = stats.GetStat(_unitConfig.BaseStatTypes.Speed);
        agent.acceleration = 120f;
        agent.angularSpeed = 720f;
        agent.stoppingDistance = 0.5f; 
        agent.obstacleAvoidanceType = ObstacleAvoidanceType.LowQualityObstacleAvoidance;
        
        unit.Initialize(attackConfig, team);

        return unitGo;
    }
}