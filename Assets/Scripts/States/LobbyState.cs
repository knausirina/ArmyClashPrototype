using UnityEngine;
using Zenject;

public class LobbyState : IState
{
    private readonly SignalBus _signalBus;
    private readonly BattleManager _battleManager;
    private readonly LevelsStorage _levelsStorage;

    public LobbyState(SignalBus signalBus, BattleManager battleManager, LevelsStorage levelsStorage)
    {
        _signalBus = signalBus;
        _battleManager = battleManager;
        _levelsStorage = levelsStorage;
    }

    public void Enter()
    {
        _signalBus.Subscribe<StartBattleSignal>(OnStartBattleSignal);
        _signalBus.Subscribe<RandomizeArmiesSignal>(OnRandomizeArmiesSignal);
    }

    public void Exit()
    {
        _signalBus.Unsubscribe<StartBattleSignal>(OnStartBattleSignal);
        _signalBus.Unsubscribe<RandomizeArmiesSignal>(OnRandomizeArmiesSignal);
    }

    private void OnStartBattleSignal()
    {
        _signalBus.Fire(ChangeStateRequestSignal.Create<BattleState>());
    }

    private void OnRandomizeArmiesSignal()
    {
        _battleManager.PrepareLevel(_levelsStorage.GetCurrentLevel());
    }
}
