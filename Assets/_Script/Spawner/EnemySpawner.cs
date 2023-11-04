using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : Singleton<EnemySpawner>
{
    [SerializeField] private List<Transform> _ghostSpawnPoints;
    [SerializeField] private List<Transform> _ratSpawnPoint;
    [SerializeField] private int _numberGhostPerPoint = 3;
    [SerializeField] private Transform _holder;

    private ObjectPool<Enemy> _ghostPool;
    private ObjectPool<Enemy> _ratPool;

    private bool _spawnEnemies = true;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadSpawnLocations();
        LoadEnemyHolder();
    }

    private void LoadSpawnLocations()
    {
        LoadGhostSpawnLocation();
        LoadRatSpawnPoints();
    }

    private void LoadGhostSpawnLocation()
    {
        _ghostSpawnPoints.Clear();

        Transform spawnPoints = transform.Find("Ghost Spawn Points");
        if (spawnPoints)
        {
            foreach (Transform spawnPoint in spawnPoints)
            {
                _ghostSpawnPoints.Add(spawnPoint);
            }
        }
    }

    private void LoadRatSpawnPoints()
    {
        _ratSpawnPoint.Clear();

        Transform spawnPoints = transform.Find("Rat Spawn Points");
        if (spawnPoints)
        {
            foreach (Transform spawnPoint in spawnPoints)
            {
                _ratSpawnPoint.Add(spawnPoint);
            }
        }
    }

    private void LoadEnemyHolder()
    {
        if (_holder != null)
        {
            return;
        }
        _holder = transform.Find("Holder");
    }

    private void Start()
    {
        InitGhostPool();
        InitRatPool();
    }

    private void Update()
    {
        if(GameManager.Instance.GameState != GameState.PhaseTwo || !EnemySpawnTrigger.Instance.EnemySpawning)
        {
            return;
        }


        if(_spawnEnemies)
        {
            SpawnGhosts();
            SpawnRats();
            _spawnEnemies = false;
        }
        
    }

    private void InitRatPool()
    {
        _ratPool = new ObjectPool<Enemy>(() =>
        {
            var ratScript = ResourceSystem.Instance.GetEnemy(EnemyType.Rat);
            return Instantiate(ratScript.Prefab);
        }, rat =>
        {
            rat.gameObject.SetActive(true);
        }, rat =>
        {
            rat.gameObject.SetActive(false);
        }, rat =>
        {
            Destroy(rat.gameObject);
        }, false, 15, 20);
    }

    private void InitGhostPool()
    {
        _ghostPool = new ObjectPool<Enemy>(() =>
        {
            var ghostScript = ResourceSystem.Instance.GetEnemy(EnemyType.Ghost);
            return Instantiate(ghostScript.Prefab);
        }, ghost =>
        {
            ghost.gameObject.SetActive(true);
        }, ghost =>
        {
            ghost.gameObject.SetActive(false);
        }, ghost =>
        {
            Destroy(ghost.gameObject);
        }, false, 15, 20);
    }

    private Enemy GetEnemy(EnemyType enemyType)
    {
        switch (enemyType)
        {
            case EnemyType.None:
                return null;
            case EnemyType.Rat:
                return _ratPool.Get();
            case EnemyType.Ghost:
                return _ghostPool.Get();
            case EnemyType.Spider:
                return null;
        }
        return null;
    }

    private ObjectPool<Enemy> GetEnemyPool(EnemyType enemyType)
    {
        switch (enemyType)
        {
            case EnemyType.None:
                break;
            case EnemyType.Rat:
                return _ratPool;
            case EnemyType.Ghost:
                return _ghostPool;
            case EnemyType.Spider:
                break;
        }
        return null;
    }

    private Enemy SpawnEnemy(EnemyType enemyType,Vector3 position)
    {
        var enemy = GetEnemy(enemyType);
        enemy.transform.SetPositionAndRotation(position, Quaternion.identity);
        var enemyScript = ResourceSystem.Instance.GetEnemy(enemyType);
        enemy.SetStats(enemyScript.BaseStats);
        enemy.SetType(enemyScript.EnemyType);
        enemy.SetPool(GetEnemyPool(enemyType));
        enemy.transform.SetParent(_holder);
        return enemy;
    }

    private void SpawnRats()
    {
        foreach (var point in _ratSpawnPoint)
        {
            SpawnEnemy(EnemyType.Rat,point.position);
        }
    }
    private void SpawnGhosts()
    {
        foreach (var point in _ghostSpawnPoints)
        {
            for (int i = 0; i < _numberGhostPerPoint; i++)
            {
                Vector3 randomPoint = Helper.RandomPositionInCircle(point.position,(int)point.localScale.x);
                SpawnEnemy(EnemyType.Ghost,randomPoint);
            }
        }
    }

}
