using UnityEngine;

[CreateAssetMenu(fileName = "ColorConfig", menuName = "ArmyClash/Configs/Color")]
public class UnitColorConfig : BaseParameterConfig
{
    [SerializeField] private Material _material;

    public void ApplyMaterial(Renderer renderer)
    {
        if (renderer == null)
            return;

        renderer.sharedMaterial = _material;
    }
}