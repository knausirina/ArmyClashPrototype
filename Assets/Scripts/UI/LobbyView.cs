
    using UnityEngine;
    using UnityEngine.UI;
    using Zenject;

    public class LobbyView : MonoBehaviour
    {
        [SerializeField] private Button _startBattleButton;
        [SerializeField] private Button _randomizeButton;

        private SignalBus _signalBus;
        
        [Inject]
        private void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }
        
        private void Awake()
        {
            _startBattleButton.onClick.AddListener(OnStartBattle);
            _randomizeButton.onClick.AddListener(OnRandomize);
        }
        
        private void OnDestroy()
        {
            _startBattleButton.onClick.RemoveAllListeners();
            _randomizeButton.onClick.RemoveAllListeners();
        }
        
        public void ToggleShow(bool isShowing)
        {
            gameObject.SetActive(isShowing);
        }
        
        private void OnStartBattle()
        {
            _signalBus.Fire<StartBattleSignal>();
            gameObject.SetActive(false);
        }

        private void OnRandomize()
        {
            _signalBus.Fire<RandomizeArmiesSignal>();
        }
    }
