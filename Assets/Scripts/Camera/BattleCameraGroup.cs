using Unity.Cinemachine;
using UnityEngine;
using Zenject;

public class BattleCameraGroup : MonoBehaviour
{
    [SerializeField] private CinemachineTargetGroup _targetGroup;
    [SerializeField] private Transform _playerProxy; 
    [SerializeField] private Transform _enemyProxy;  
    [SerializeField] private float _smoothSpeed = 5f;
    
    [SerializeField] private float _playerWeight = 2f; 
    [SerializeField] private float _enemyWeight = 1f;  

    private UnitStorage _unitStorage;

    [Inject]
    private void Construct(UnitStorage unitStorage)
    {
        _unitStorage = unitStorage;
    }

    private void Start()
    {
        SetupTargetGroup();
        UpdateProxyInstant(Team.Player, _playerProxy);
        UpdateProxyInstant(Team.Enemy, _enemyProxy);
    }
    
    private void UpdateProxyInstant(Team team, Transform proxy)
    {
        var units = _unitStorage.GetTeam(team);
        if (units != null && units.Count > 0)
            proxy.position = units[0].transform.position;
    }

    private void LateUpdate()
    {
        UpdateProxy(Team.Player, _playerProxy);
        UpdateProxy(Team.Enemy, _enemyProxy);
    }

    private void UpdateProxy(Team team, Transform proxy)
    {
        var units = _unitStorage.GetTeam(team);
        if (units == null)
            return;

        var sum = Vector3.zero;
        var aliveCount = 0;

        foreach (var unity in units)
        {
            if (unity != null && !unity.IsDead)
            {
                sum += unity.transform.position;
                aliveCount++;
            }
        }

        var targetWeight = 0.001f; 
        var maxDistance = 1f;
        var padding = 2f;

        var center = sum / aliveCount;
        if (aliveCount > 0)
        {
            proxy.position = Vector3.Lerp(proxy.position, center, 1f - Mathf.Exp(-_smoothSpeed * Time.deltaTime));

            targetWeight = team.IsPlayer() ? _playerWeight : _enemyWeight;
            
            foreach (var unity in units)
            {
                if (unity != null && !unity.IsDead)
                    maxDistance = Mathf.Max(maxDistance, Vector3.Distance(center, unity.transform.position) + padding);
            }
        }

        UpdateTargetGroupMember(proxy, targetWeight, maxDistance);
    }

    private void SetupTargetGroup()
    {
        if (_targetGroup == null)
            return;
        
        _targetGroup.Targets.Clear();
        _targetGroup.AddMember(_playerProxy, _playerWeight, 1f);
        _targetGroup.AddMember(_enemyProxy, _enemyWeight, 1f);
    }

    private void UpdateTargetGroupMember(Transform target, float weight, float radius)
    {
        var index = _targetGroup.FindMember(target);
        if (index != -1)
        {
            var t = _targetGroup.Targets[index];
            t.Weight = weight;
            t.Radius = radius;
            _targetGroup.Targets[index] = t;
        }
    }
}