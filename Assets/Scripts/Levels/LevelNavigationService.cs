using Zenject;

public class LevelNavigationService
{
    private readonly LevelsStorage _levelsStorage;
    private readonly BattleManager _battleManager;
    private readonly BattleResultService _resultService;
    private readonly SignalBus _signalBus;

    public LevelNavigationService(LevelsStorage levelsStorage, BattleManager battleManager, BattleResultService battleResultService,
        SignalBus signalBus)
    {
        _levelsStorage = levelsStorage;
        _battleManager = battleManager;
        _resultService = battleResultService;
        _signalBus = signalBus;
    }

    public void OnNextLevelRequest()
    {
        if (_resultService.LastWinner == Team.Player)
        {
            if (_levelsStorage.IsLastLevel())
            {
                _levelsStorage.ResetToStart();
            }
            _levelsStorage.SetNext();
        }

        _battleManager.PrepareLevel(_levelsStorage.GetCurrentLevel());
        _signalBus.Fire(ChangeStateRequestSignal.Create<LobbyState>());
    }
}  