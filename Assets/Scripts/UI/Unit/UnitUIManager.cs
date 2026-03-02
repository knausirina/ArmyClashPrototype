using UnityEngine;
using System.Collections.Generic;
using Zenject;

public class UnitUIManager : MonoBehaviour
{
    [SerializeField] private RectTransform _container;
    [SerializeField] private Vector3 _worldOffset = new Vector3(-4, 1.2f, 0);

    private SignalBus _signalBus;
    private UnitStorage _unitStorage;
    private UnitStatsViewPool _statsPool;

    private readonly Dictionary<Unit, UnitStatsView> _activeIndicators = new();
    private Camera _camera;
    private readonly Team[] _teams = (Team[])System.Enum.GetValues(typeof(Team));
    private readonly List<Unit> _removeQueue = new();

    private bool _isEnabled = false;
    private bool _isBattleStarted = false;

    [Inject]
    private void Construct(SignalBus signalBus, UnitStatsViewPool statsPool, UnitStorage unitStorage)
    {
        _signalBus = signalBus;
        _statsPool = statsPool;
        _unitStorage = unitStorage;
    }

    private void Awake()
    {
        _camera = Camera.main;
        _signalBus.Subscribe<GameStateChangedSignal>(OnStateChanged);
    }

    private void OnDestroy()
    {
        _signalBus.Unsubscribe<GameStateChangedSignal>(OnStateChanged);
    }

    private void OnStateChanged(GameStateChangedSignal signal)
    {
        _isEnabled = signal.NewState == typeof(LobbyState) || signal.NewState == typeof(BattleState);
        _isBattleStarted = signal.NewState == typeof(BattleState);

        if (!_isEnabled)
        {
            ClearAll();
        }
    }

    private void LateUpdate()
    {
        if (!_isEnabled || !_isBattleStarted || !EnsureCamera())
            return;

        CleanupDeadUnits();

        foreach (Team team in _teams)
        {
            var units = _unitStorage.GetTeam(team);
            for (int i = 0; i < units.Count; i++)
            {
                UpdateIndicator(units[i]);
            }
        }
    }

    private void UpdateIndicator(Unit unit)
    {
        if (unit == null || unit.IsDead)
            return;

        if (_camera == null)
            return;

        if (!_activeIndicators.TryGetValue(unit, out var indicator))
        {
            indicator = GetFromPool();
            _activeIndicators.Add(unit, indicator);
            indicator.SetData(unit);
        }

        var screenPos = _camera.WorldToScreenPoint(unit.transform.position +  _worldOffset);
       
        if (screenPos.z > 0)
        { 
            indicator.gameObject.SetActive(true);
            indicator.transform.position = screenPos;
            indicator.transform.localScale = Vector3.one;
        }
        else
        {
            indicator.gameObject.SetActive(false);
        }
    }

    private UnitStatsView GetFromPool()
    {
        var view = _statsPool.Spawn();
        view.transform.SetParent(_container, false); 
        return view;
    }

    private bool EnsureCamera()
    {
        if (_camera != null)
            return true;

        _camera = Camera.main;
        return _camera != null;
    }

    private void ClearAll()
    {
        foreach (var indicator in _activeIndicators.Values)
        {
            indicator.Clear();
            indicator.gameObject.SetActive(false);
            _statsPool.Despawn(indicator);
        }
        _activeIndicators.Clear();
    }

    private void CleanupDeadUnits()
    {
        _removeQueue.Clear();

        foreach (var pair in _activeIndicators)
        {
            if (pair.Key == null || pair.Key.IsDead)
            {
                _removeQueue.Add(pair.Key);
            }
        }

        foreach (var unit in _removeQueue)
        {
            var indicator = _activeIndicators[unit];
            indicator.Clear();
            indicator.gameObject.SetActive(false);
            _statsPool.Despawn(indicator);
            _activeIndicators.Remove(unit);
        }
    }
}
