    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;
    using Zenject;

    public class ResultView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _resultText;
        [SerializeField] private Button _closeButton;

        private SignalBus _signalBus;
        private BattleResultService _battleResultService;

        private const string Win = "Win!";
        private const string Lose = "Lose...";

        [Inject]
        private void Construct(SignalBus signalBus, BattleResultService battleResultService)
        {
            _signalBus = signalBus;
            _battleResultService = battleResultService;
        }
        
        public void ToggleShow(bool isShowing)
        {
            gameObject.SetActive(isShowing);
        }

        private void Awake()
        {
            _closeButton.onClick.AddListener(OnCloseButton);
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            _closeButton.onClick.RemoveListener(OnCloseButton);
        }

        private void OnEnable()
        {
            _resultText.text = _battleResultService.IsWin()? Win : Lose;
        }

        private void OnCloseButton()
        {
            gameObject.SetActive(false);
            
            _signalBus.Fire(ChangeStateRequestSignal.Create<MainMenuState>());
        }
    }
