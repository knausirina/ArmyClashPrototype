using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MainMenuView : MonoBehaviour
{
    [SerializeField] private Button _playButton;

    private SignalBus _signalBus;

    [Inject]
    private void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    private void Awake()
    {
        _playButton.onClick.AddListener(OnPlayButton);
    }
    
    private void OnDestroy()
    {
        _playButton.onClick.RemoveAllListeners();
    }

    public void ToggleShow(bool isShowing)
    {
        gameObject.SetActive(isShowing);
    }

    private void OnPlayButton()
    {
        gameObject.SetActive(false);
        _signalBus.Fire<NextLevelRequestedSignal>();
    }
}