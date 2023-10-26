using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : Singleton<EnemySpawner>
{
    [SerializeField] private List<Transform> _ghostSpawnPoints;
    [SerializeField] private List<Transform> _snakeSpawnPoints;
    [SerializeField] private int _numberGhostPerPoint = 3;
    [SerializeField] private Transform _holder;

    private ObjectPool<Enemy> _ghostPool;
    private ObjectPool<Enemy> _snakePool;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadSpawnLocations();
        LoadEnemyHolder();
    }

    private void LoadSpawnLocations()
    {
        LoadGhostSpawnLocation();
        LoadSnakeSpawnLocation();
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

    private void LoadSnakeSpawnLocation()
    {
        _snakeSpawnPoints.Clear();

        Transform spawnPoints = transform.Find("Snake Spawn Points");
        if (spawnPoints)
        {
            foreach (Transform spawnPoint in spawnPoints)
            {
                _snakeSpawnPoints.Add(spawnPoint);
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
        InitSnakePool();
        if(GameManager.Instance.GameState == GameState.PhaseTwo)
        {
            SpawnGhosts();
            SpawnSnakes();
        }
    }

    private void InitSnakePool()
    {
        _snakePool = new ObjectPool<Enemy>(() =>
        {
            var snakeScript = ResourceSystem.Instance.GetEnemy(EnemyType.Snake);
            return Instantiate(snakeScript.Prefab);
        }, snake =>
        {
            snake.gameObject.SetActive(true);
        }, snake =>
        {
            snake.gameObject.SetActive(false);
        }, snake =>
        {
            Destroy(snake.gameObject);
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
        }, false,15,20);
    }

    private Enemy GetEnemy(EnemyType enemyType)
    {
        switch (enemyType)
        {
            case EnemyType.None:
                return null;
            case EnemyType.Snake:
                return _snakePool.Get();
            case EnemyType.Ghost:
                return _ghostPool.Get();
            case EnemyType.Spider:
                return null;
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
        enemy.SetPool(_ghostPool);
        enemy.transform.SetParent(_holder);
        return enemy;
    }

    private void SpawnSnakes()
    {
        foreach (var point in _snakeSpawnPoints)
        {
            SpawnEnemy(EnemyType.Snake,point.position);
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
