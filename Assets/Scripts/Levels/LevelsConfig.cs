using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelsConfig", menuName = "ArmyClash/LevelsConfig")]
public class LevelsConfig : ScriptableObject
{
    [Serialize] public List<LevelConfig> _levels;
    
    public IReadOnlyList<LevelConfig> Levels => _levels;
}