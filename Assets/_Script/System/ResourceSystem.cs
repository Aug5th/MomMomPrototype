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
    [SerializeField] private List<ProjectileScriptableObject> _projectiles = new();
    public List<EnemyScriptableObject> Enemies => _enemies;
    public List<ToyScriptableObject> Toys => _toys;
    public List<TileScriptableObject> Tiles => _tiles;
    public List<ProjectileScriptableObject> Projectiles => _projectiles;

    private Dictionary<EnemyType, EnemyScriptableObject> _enemyDict;
    private Dictionary<ToyType, ToyScriptableObject> _toyDict;
    private Dictionary<TileBase, TileScriptableObject> _tileDict;
    private Dictionary<ProjectileType, ProjectileScriptableObject> _projectileDict;

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
        LoadProjectiles();
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
        _tileDict = new Dictionary<TileBase, TileScriptableObject>();
       foreach(var tileData in _tiles)
       {
            foreach(var tile in tileData.tile)
            {
                _tileDict.Add(tile, tileData);
            }
       }
    }
    private void LoadProjectiles()
    {
        _projectiles = Resources.LoadAll<ProjectileScriptableObject>("Projectile").ToList();
        _projectileDict = _projectiles.ToDictionary(r => r.ProjectileType, r => r);
    }

    public EnemyScriptableObject GetEnemy(EnemyType type) => _enemyDict[type];
    public ToyScriptableObject GetToy(ToyType type) => _toyDict[type];
    public  TileType GetTile(TileBase tile)
    {
        return _tileDict[tile].tileType;
    }
    public ProjectileScriptableObject GetProjectile(ProjectileType type) => _projectileDict[type];
}

