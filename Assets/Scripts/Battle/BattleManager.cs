using System;
using UnityEngine;
using Zenject;

public class BattleManager : IDisposable
{
    private readonly SignalBus _signalBus;
    private readonly IUnitFactory _unitFactory;
    private readonly UnitStorage _unitStorage;
    private readonly UnitStatsStorage _unitStatsStorage;
    private readonly UnitConfig _unitConfig;
    private readonly BattleResultService _battleResultService;

    private bool _battleStarted = false;

    public BattleManager(IUnitFactory unitFactory,
        UnitStorage unitStorage,
        UnitStatsStorage unitStatsStorage,
        UnitConfig unitConfig,
        BattleResultService battleResultService,
        SignalBus signalBus)
    {
        _signalBus = signalBus;
        _unitFactory = unitFactory;
        _unitStorage = unitStorage;
        _unitStatsStorage = unitStatsStorage;
        _unitConfig = unitConfig;
        _battleResultService = battleResultService;
    }
    
    public void PrepareLevel(LevelConfig level)
    {
        if (_battleStarted)
            return;
        Debug.Log("PrepareLevel");
        _battleResultService.LastWinner = null;
        
        ClearArmies();

        foreach (var army in level.Armies)
        {
            SpawnTeam(army);
        }
    }
    
    public void StartBattle()
    {
        if (_battleStarted) return;
        
        _battleStarted = true;
        
        _unitStorage.OnTeamEmpty -= OnTeamEmpty;
        _unitStorage.OnTeamEmpty += OnTeamEmpty;

        foreach (Team team in Enum.GetValues(typeof(Team)))
        {
            var units = _unitStorage.GetTeam(team);
            foreach (var unit in units)
            {
                unit.SetBattleState(UnitMode.Battle);
            }
        }
    }

    public void Dispose()
    {
        _unitStorage.OnTeamEmpty -= OnTeamEmpty;
    }
    
    public void ClearArmies()
    {
        _battleStarted = false;
        _unitStorage.OnTeamEmpty -= OnTeamEmpty;

        foreach (Team team in Enum.GetValues(typeof(Team)))
        {
            var units = _unitStorage.GetTeam(team);
            for (int i = units.Count - 1; i >= 0; i--)
            {
                if (units[i] != null)
                {
                    UnityEngine.Object.Destroy(units[i].gameObject);
                }
            }
        }
        _unitStorage.ClearAll();
        _unitStatsStorage.Clear();
    }

    private void OnTeamEmpty(Team emptyTeam)
    {
        if (!_battleStarted)
            return;
        
        var winner = emptyTeam.GetOpposite(); 
        OnBattleEnd(winner);
    }

    private void SpawnTeam(ArmySpawnData armySpawnData)
    {
        var strategy = armySpawnData.Formation.CreateStrategy();
        var commands = strategy.GetDeployment(armySpawnData.Count);

        Quaternion rotation = Quaternion.LookRotation(armySpawnData.LookDirection);
        
        foreach (var command in commands)
        {
            Vector3 localPos = command.Position;
            Vector3 rotatedPos = rotation * localPos;
            
            Vector3 finalPos = armySpawnData.BasePosition + rotatedPos;
            
            var unitObj = _unitFactory.CreateUnit(
                _unitConfig.GetRandomShape(), 
                _unitConfig.GetRandomSize(), 
                _unitConfig.GetRandomColor(), 
                _unitConfig.GetRandomAttack(), 
                armySpawnData.Team, 
                finalPos);

            unitObj.transform.rotation = Quaternion.LookRotation(armySpawnData.LookDirection);
            
            if (unitObj.TryGetComponent<Unit>(out var unit))
            {
                _unitStorage.Add(unit);
            }
            else
            {
                Debug.LogError($"[BattleManager] {unitObj.name} not exist component Unit!", unitObj);
            }
        }
    }

    private void OnBattleEnd(Team winner)
    {
        _battleStarted = false;
        _unitStorage.OnTeamEmpty -= OnTeamEmpty;
        _battleResultService.LastWinner = winner;

        _signalBus.Fire(ChangeStateRequestSignal.Create<ResultState>());
    }
}
