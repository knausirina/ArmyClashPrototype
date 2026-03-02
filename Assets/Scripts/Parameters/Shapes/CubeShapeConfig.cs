using UnityEngine;

[CreateAssetMenu(fileName = "CubeShape", menuName = "ArmyClash/Shapes/Cube Shape")]
public class CubeShapeConfig : UnitShapeConfig
{
    [SerializeField] private GameObject _cubePrefab;

    public override GameObject CreateVisual(Transform parent)
    {
        var visual = Instantiate(_cubePrefab, parent);
        visual.transform.localScale = Vector3.one;
        visual.transform.localPosition = new Vector3(0, 0.5f, 0);
        return visual;
    }
}