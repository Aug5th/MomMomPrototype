using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Projectile", menuName = "Scriptable Object / Projectile")]
public class ProjectileScriptableObject : ScriptableObject
{
    [SerializeField] private ProjectileStats _stats;
    public ProjectileStats BaseStats => _stats;

    public ProjectileType ProjectileType;

    public Projectile Prefab;
}

[Serializable]
public struct ProjectileStats
{
    public float Power;
    public float Speed;
    public bool  Pierceable;
}

public enum ProjectileType
{
    None = 0,
    Arrow = 1,
    Bullet = 2,
}