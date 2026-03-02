using System;
using UnityEngine;

[Serializable]
public class ArmySpawnData
{
    [SerializeField] private Team _team;
    [SerializeField] private Vector3 _basePosition;
    [SerializeField] private Vector3 _lookDirection = Vector3.forward;
    [SerializeField] private FormationConfig _formation;
    [SerializeField] private int _count;

    public Team Team => _team;
    public Vector3 BasePosition => _basePosition;
    public Vector3 LookDirection => _lookDirection;
    public FormationConfig Formation => _formation;
    public int Count => _count;
}