using UnityEngine;
using Zenject;

public class MainMenuState : IState
{
    private readonly SignalBus _signalBus;
    private readonly UnitStorage _unitStorage;
    private readonly LevelNavigationService _levelNavigationService;

    public MainMenuState(SignalBus signalBus, UnitStorage unitStorage, LevelNavigationService levelNavigationService)
    {
        _signalBus = signalBus;
        _unitStorage = unitStorage;
        _levelNavigationService = levelNavigationService;
    }

    public void Enter()
    {
        _signalBus.Subscribe<NextLevelRequestedSignal>(OnNextLevelRequest);
        _unitStorage.ClearAll();
    }

    public void Exit()
    {
        _signalBus.Unsubscribe<NextLevelRequestedSignal>(OnNextLevelRequest);
    }
    
    private void OnNextLevelRequest() 
    {
        _levelNavigationService.OnNextLevelRequest(); 
    }
}