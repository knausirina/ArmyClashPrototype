using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "ArmyClash/LevelConfig")]
public class LevelConfig : ScriptableObject
{
    [SerializeField] public List<ArmySpawnData> _armies;
    
    public IReadOnlyList<ArmySpawnData> Armies => _armies;
}