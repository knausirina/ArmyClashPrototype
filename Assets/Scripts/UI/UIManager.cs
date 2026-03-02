using UnityEngine;using Zenject;

public class UIManager : MonoBehaviour
{
    [SerializeField] private MainMenuView _mainMenuView;
    [SerializeField] private LobbyView  _lobbyView;
    [SerializeField] private ResultView _resultView;

    private SignalBus _signalBus;

    [Inject]
    private void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
        _signalBus.Subscribe<GameStateChangedSignal>(OnStateChanged);
    }

    private void OnStateChanged(GameStateChangedSignal signal)
    {
        _mainMenuView.ToggleShow(false);
        _lobbyView.ToggleShow(false);
        _resultView.ToggleShow(false);
        

        if (signal.NewState == typeof(MainMenuState))
            _mainMenuView.ToggleShow(true);
        else if (signal.NewState == typeof(LobbyState))
            _lobbyView.ToggleShow(true);
        else if (signal.NewState == typeof(ResultState))
            _resultView.ToggleShow(true);
    }

    private void OnDestroy()
    {
        _signalBus?.TryUnsubscribe<GameStateChangedSignal>(OnStateChanged);
    }
}