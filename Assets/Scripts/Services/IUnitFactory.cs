using UnityEngine;

    public interface IUnitFactory
    {
        GameObject CreateUnit(UnitShapeConfig shape, UnitSizeConfig size, UnitColorConfig color, 
            UnitAttackConfig attack, Team team, Vector3 position);
    }