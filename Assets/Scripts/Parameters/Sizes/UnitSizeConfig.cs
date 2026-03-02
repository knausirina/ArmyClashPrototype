using UnityEngine;


    [CreateAssetMenu(fileName = "UnitSize", menuName = "ArmyClash/Sizes/Unit Size")]
    public class UnitSizeConfig : BaseParameterConfig
    {
        [field: SerializeField] public float SizeScale { get; private set; } = 1f;

        public virtual void ApplyScale(Transform visualTransform)
        {
            visualTransform.localScale = Vector3.one * SizeScale;
        }
    }