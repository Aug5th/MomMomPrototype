using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Enemy" , menuName ="Scriptable Object / Enemy")]
public class EnemyScriptableObject : ScriptableObject
{
    [SerializeField] private EnemyStats _stats;
    public EnemyStats BaseStats
        => _stats;

    public EnemyType EnemyType;

    public Enemy Prefab;
}

[Serializable]
public struct EnemyStats
{
    public float HealthPoint;
    public float Armor;
    public float Power;
    public float AttackSpeed;
    public float AttackRange;
    public float MovementSpeed;
    public bool  AttackPlayerOnly;
}

public enum EnemyType
{
    None = 0,
    Ghost = 1,
    Tanker = 2,
    Melee = 3
}