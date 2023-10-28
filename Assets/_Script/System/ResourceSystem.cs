using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ResourceSystem : Singleton<ResourceSystem>
{
    [SerializeField] private List<EnemyScriptableObject> _enemies = new();
    [SerializeField] private List<ToyScriptableObject> _toys = new();
    [SerializeField] private List<TileScriptableObject> _tiles = new();
    public List<EnemyScriptableObject> Enemies => _enemies;
    public List<ToyScriptableObject> Toys => _toys;
    public List<TileScriptableObject> Tiles => _tiles;

    private Dictionary<EnemyType, EnemyScriptableObject> _enemyDict;
    private Dictionary<ToyType, ToyScriptableObject> _toyDict;
    private Dictionary<TileType, TileScriptableObject> _tileDict;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        AssembleResources();
    }

    private void AssembleResources()
    {
        LoadEnemies();
        LoadToys();
        LoadTiles();
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

    private void LoadTiles()
    {
        _tiles = Resources.LoadAll<TileScriptableObject>("Tile").ToList();
        _tileDict = _tiles.ToDictionary(r => r.TileType, r => r);
    }

    public EnemyScriptableObject GetEnemy(EnemyType type) => _enemyDict[type];
    public ToyScriptableObject GetToy(ToyType type) => _toyDict[type];
    public TileBase GetTile(TileType type) => _tileDict[type].tile;
}

