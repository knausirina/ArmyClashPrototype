using UnityEngine;

[CreateAssetMenu(fileName = "SplitFormation", menuName = "ArmyClash/Formations/Split")]
public class SplitFormationConfig : FormationConfig
{
    public override IFormationStrategy CreateStrategy()
    {
        return new SplitFormationStrategy(this);
    }
}