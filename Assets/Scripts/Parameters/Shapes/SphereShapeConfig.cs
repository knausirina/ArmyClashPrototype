using UnityEngine;

[CreateAssetMenu(fileName = "SphereShape", menuName = "ArmyClash/Shapes/Sphere Shape")]
public class SphereShapeConfig : UnitShapeConfig
{
    [SerializeField] private GameObject _spherePrefab;

    public override GameObject CreateVisual(Transform parent)
    {
        var visual = Instantiate(_spherePrefab, parent);
        visual.transform.localPosition = new Vector3(0, 0.5f, 0);
        return visual;
    }
}