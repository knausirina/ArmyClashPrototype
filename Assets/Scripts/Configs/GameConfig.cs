using UnityEngine;

[CreateAssetMenu(fileName = "Config", menuName = "ArmyClash/Config")]
public class GameConfig : ScriptableObject
{
   [field: SerializeField] public UnitConfig UnitConfig { get; private set; }
   [field: SerializeField] public LevelsConfig LevelsConfig{ get; private set; }
}