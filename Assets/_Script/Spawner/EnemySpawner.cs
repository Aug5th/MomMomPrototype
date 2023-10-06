using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : Singleton<EnemySpawner>
{
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private int quantityPerPoint;
    [SerializeField] private Transform _holder;

    private ObjectPool<Enemy> _enemyPool;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadSpawnLocations();
        LoadEnemyHolder();
    }

    private void LoadSpawnLocations()
    {
        if(_spawnPoints.Count > 0)
        {
            return;
        }

        Transform spawnPoints = transform.Find("Spawn Points"); 
        if (spawnPoints)
        {
            foreach (Transform spawnPoint in spawnPoints)
            {
                _spawnPoints.Add(spawnPoint);
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
        InitEnemyPool();
        SpawnEnemies();
    }

    private void InitEnemyPool()
    {
        _enemyPool = new ObjectPool<Enemy>(() =>
        {
            var enemyScript = ResourceSystem.Instance.GetEnemy(EnemyType.Ghost);
            return Instantiate(enemyScript.Prefab);
        }, enemy =>
        {
            enemy.gameObject.SetActive(true);
        }, enemy =>
        {
            enemy.gameObject.SetActive(false);
        },enemy =>
        {
            Destroy(enemy.gameObject);
        }, false,15,20);
    }

    private Enemy SpawnEnemy(Vector3 position)
    {
        var enemy = _enemyPool.Get();
        enemy.transform.SetPositionAndRotation(position, Quaternion.identity);
        var enemyScript = ResourceSystem.Instance.GetEnemy(EnemyType.Ghost);
        enemy.SetStats(enemyScript.BaseStats);
        enemy.SetType(enemyScript.EnemyType);
        enemy.SetPool(_enemyPool);
        return enemy;
    }

    private void SpawnEnemies()
    {
        foreach (var point in _spawnPoints)
        {
            for (int i = 0; i < quantityPerPoint; i++)
            {
                Vector3 randomPoint = Helper.RandomPositionInCircle(point.position,(int)point.localScale.x);
                SpawnEnemy(randomPoint);
            }
        }
    }

}
