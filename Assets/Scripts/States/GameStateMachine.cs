using System;
using System.Collections.Generic;
using Zenject;

public class GameStateMachine : IInitializable, IDisposable
{
    private readonly Dictionary<Type, IState> _states;
    private readonly SignalBus _signalBus;
    private IState _currentState;

    public GameStateMachine(
        MainMenuState mainMenu, LobbyState lobby, 
        BattleState battle, ResultState result, 
        SignalBus signalBus)
    {
        _signalBus = signalBus;
        _states = new Dictionary<Type, IState> {
            { typeof(MainMenuState), mainMenu },
            { typeof(LobbyState), lobby },
            { typeof(BattleState), battle },
            { typeof(ResultState), result }
        };
    }

    public void Initialize()
    {
        _signalBus.Subscribe<ChangeStateRequestSignal>(OnStateRequest);
        
        ChangeState(typeof(MainMenuState));
    }

    private void ChangeState(Type type)
    {
        if (type == null)
            return;
        if (!_states.TryGetValue(type, out var nextState))
            return;
        if (_currentState == nextState)
            return;

        _currentState?.Exit();
        _currentState = nextState;
        _currentState.Enter();

        _signalBus.Fire(new GameStateChangedSignal(type));
    }

    private void OnStateRequest(ChangeStateRequestSignal signal)
    {
        ChangeState(signal.TargetState);
    }

    public void Dispose()
    {
        _signalBus.Unsubscribe<ChangeStateRequestSignal>(OnStateRequest);
    }
}
