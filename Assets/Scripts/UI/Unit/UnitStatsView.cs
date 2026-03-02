using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UnitStatsView : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Color _playerColor = Color.green;
    [SerializeField] private Color _enemyColor = Color.red;
    [SerializeField] private TMP_Text _hpText;
    [SerializeField] private TMP_Text _atkText;
    [SerializeField] private TMP_Text _speedText;
    [SerializeField] private TMP_Text _atkSpeedText;

    private const string Hp = "Hp: ";
    private const string Atk = "Atk: ";
    private const string Speed = "Speed: ";
    private const string AtkSpeed = "AtkSpeed: ";
    
    private Unit _unit;

    private UnitStatsStorage _unitStatsStorage;
    private UnitConfig _unitConfig;

    [Inject]
    private void Construct(UnitStatsStorage unitStatsStorage, UnitConfig unitConfig)
    {
        _unitStatsStorage = unitStatsStorage;
        _unitConfig = unitConfig;

    }

    public void SetData(Unit unit)
    {
        Clear();
        _unit = unit;

        _image.color = _unit.Team.IsPlayer() ? _playerColor : _enemyColor;
        
        _unit.ChangedParameters += OnChangedParameters;
        OnChangedParameters();
    }

    public void Clear()
    {
        if (_unit != null)
            _unit.ChangedParameters -= OnChangedParameters;
    }

    private void OnChangedParameters()
    {
        var atk = _unitStatsStorage.GetStat(_unit, _unitConfig.BaseStatTypes.Atk);
        var speed = _unitStatsStorage.GetStat(_unit, _unitConfig.BaseStatTypes.Speed);
        var atkSpeed = _unitStatsStorage.GetStat(_unit, _unitConfig.BaseStatTypes.AtkSpd);
        
        _hpText.text = Hp + _unit.CurrentHp.ToString(CultureInfo.InvariantCulture);
        _atkText.text = Atk  + atk.ToString(CultureInfo.InvariantCulture);
        _speedText.text = Speed + speed.ToString(CultureInfo.InvariantCulture);
        _atkSpeedText.text = AtkSpeed + atkSpeed.ToString(CultureInfo.InvariantCulture);
    }

    private void OnDestroy()
    {
        Clear();
    }
}