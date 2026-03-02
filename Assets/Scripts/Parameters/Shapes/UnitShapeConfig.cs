using UnityEngine;

public abstract class UnitShapeConfig : BaseParameterConfig
{
    public abstract GameObject CreateVisual(Transform parent);
}