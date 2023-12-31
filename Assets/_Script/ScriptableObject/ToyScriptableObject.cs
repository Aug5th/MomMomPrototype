using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Toy", menuName = "Scriptable Object / Toy")]
public class ToyScriptableObject : ScriptableObject
{
    [SerializeField] private ToyStats _stats;
    public ToyStats BaseStats => _stats;

    public ToyType ToyType;

    public Toy Prefab;
}

[Serializable]
public struct ToyStats
{
    public float HealthPoint;
    public float Power;
    public float AttackSpeed;
    public float AttackRange;
    public float MovementSpeed;
    public float  HealingStrength;
}

public enum ToyType
{
    None = 0,
    Ranger = 1,
    Knight = 2,
    Phoenix = 3
}