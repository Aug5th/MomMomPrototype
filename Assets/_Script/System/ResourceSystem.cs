using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceSystem : Singleton<ResourceSystem>
{
    [SerializeField] private List<EnemyScriptableObject> _enemies = new();
    [SerializeField] private List<ToyScriptableObject> _toys = new();
    public List<EnemyScriptableObject> Enemies => _enemies;
    public List<ToyScriptableObject> Toys => _toys;

    private Dictionary<EnemyType, EnemyScriptableObject> _enemyDict;
    private Dictionary<ToyType, ToyScriptableObject> _toyDict;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        AssembleResources();
    }

    private void AssembleResources()
    {
        LoadEnemies();
        LoadToys();
    }
    private void LoadEnemies()
    {
        _enemies = Resources.LoadAll<EnemyScriptableObject>("Enemy").ToList();
        _enemyDict = _enemies.ToDictionary(r => r.EnemyType, r => r);
    }

    private void LoadToys()
    {
        _toys = Resources.LoadAll<ToyScriptableObject>("Toy").ToList();
        _toyDict = _toys.ToDictionary(r => r.ToyType, r => r);
    }

    public EnemyScriptableObject GetEnemy(EnemyType type) => _enemyDict[type];
    public ToyScriptableObject GetToy(ToyType type) => _toyDict[type];
}

