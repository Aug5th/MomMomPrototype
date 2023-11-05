using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ToySpawner : Singleton<ToySpawner>
{
    [SerializeField] private List<Transform> _knightSpawnPoint;
    [SerializeField] private List<Transform> _rangerSpawnPoint;
    [SerializeField] private List<Transform> _teddySpawnPoint;
    [SerializeField] private GameObject _teddy;
    [SerializeField] private Transform _holder;

    private ObjectPool<Toy> _knightPool;
    private ObjectPool<Toy> _rangerPool;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadSpawnLocations();
        LoadHolder();
    }

    private void LoadSpawnLocations()
    {
        LoadRangerSpawnPoints();
        LoadKnightSpawnPoints();
        LoadTeddySpawnPoints();
    }

    private void LoadRangerSpawnPoints()
    {
        _rangerSpawnPoint.Clear();

        Transform spawnPoints = transform.Find("Ranger Spawn Points");
        if (spawnPoints)
        {
            foreach (Transform spawnPoint in spawnPoints)
            {
                _rangerSpawnPoint.Add(spawnPoint);
            }
        }
    }

    private void LoadKnightSpawnPoints()
    {
        _knightSpawnPoint.Clear();

        Transform spawnPoints = transform.Find("Knight Spawn Points");
        if (spawnPoints)
        {
            foreach (Transform spawnPoint in spawnPoints)
            {
                _knightSpawnPoint.Add(spawnPoint);
            }
        }
    }

    private void LoadTeddySpawnPoints()
    {
        _teddySpawnPoint.Clear();

        Transform spawnPoints = transform.Find("Teddy Spawn Points");
        if (spawnPoints)
        {
            foreach (Transform spawnPoint in spawnPoints)
            {
                _teddySpawnPoint.Add(spawnPoint);
            }
        }
    }

    private void LoadHolder()
    {
        if (_holder != null)
        {
            return;
        }
        _holder = transform.Find("Holder");
    }
    private void Start()
    {
        InitKnightPool();
        InitRangerPool();

        SpawnKnights();
        SpawnRangers();
        SpawnTeddy();
    }

    private void SpawnKnights()
    {
        foreach (var point in _knightSpawnPoint)
        {
            SpawnKnight(point.position); 
        }
    }

    private void SpawnRangers()
    {
        foreach (var point in _rangerSpawnPoint)
        {
            SpawnRanger(point.position);
        }
    }

    private void SpawnTeddy()
    {
        GameObject teddy = Instantiate(_teddy, Kid.Instance.transform.position, Quaternion.identity);
    }

    private Toy SpawnKnight(Vector3 position)
    {
        var knight = _knightPool.Get();
        knight.transform.SetPositionAndRotation(position, Quaternion.identity);
        var toyScript = ResourceSystem.Instance.GetToy(ToyType.Knight);
        knight.SetStats(toyScript.BaseStats);
        knight.SetType(toyScript.ToyType);
        knight.SetPool(_knightPool);
        knight.transform.SetParent(_holder);
        return knight;
    }

    private Toy SpawnRanger(Vector3 position)
    {
        var ranger = _rangerPool.Get();
        ranger.transform.SetPositionAndRotation(position, Quaternion.identity);
        var toyScript = ResourceSystem.Instance.GetToy(ToyType.Ranger);
        ranger.SetStats(toyScript.BaseStats);
        ranger.SetType(toyScript.ToyType);
        ranger.SetPool(_rangerPool);
        ranger.transform.SetParent(_holder);
        return ranger;
    }

    private void InitKnightPool()
    {
        _knightPool = new ObjectPool<Toy>(() =>
        {
            var toyScript = ResourceSystem.Instance.GetToy(ToyType.Knight);
            return Instantiate(toyScript.Prefab);
        }, knight =>
        {
            knight.gameObject.SetActive(true);
        }, knight =>
        {
            knight.gameObject.SetActive(false);
        }, knight =>
        {
            Destroy(knight.gameObject);
        }, false, 5, 10);
    }
    private void InitRangerPool()
    {
        _rangerPool = new ObjectPool<Toy>(() =>
        {
            var toyScript = ResourceSystem.Instance.GetToy(ToyType.Ranger);
            return Instantiate(toyScript.Prefab);
        }, ranger =>
        {
            ranger.gameObject.SetActive(true);
        }, ranger =>
        {
            ranger.gameObject.SetActive(false);
        }, ranger =>
        {
            Destroy(ranger.gameObject);
        }, false, 5, 10);
    }
}
