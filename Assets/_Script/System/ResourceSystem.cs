using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceSystem : Singleton<ResourceSystem>
{
    [SerializeField] private List<EnemyScriptableObject> _enemies = new();
    public List<EnemyScriptableObject> Enemies => _enemies;
    private Dictionary<EnemyType, EnemyScriptableObject> _enemyDict;    
    protected override void LoadComponents()
    {
        base.LoadComponents();
        AssembleResources();
    }

    private void AssembleResources()
    {
        LoadEnemies();
    }

    private void LoadEnemies()
    {
        _enemies = Resources.LoadAll<EnemyScriptableObject>("Enemy").ToList();
        _enemyDict = _enemies.ToDictionary(r => r.EnemyType , r => r);
    }

    public EnemyScriptableObject GetEnemy(EnemyType type) => _enemyDict[type];
}

