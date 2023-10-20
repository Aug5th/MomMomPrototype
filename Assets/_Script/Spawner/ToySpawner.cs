using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ToySpawner : Singleton<ToySpawner>
{
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private Transform _holder;

    private ObjectPool<Toy> _knightPool;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadSpawnLocations();
        LoadHolder();
    }

    private void LoadSpawnLocations()
    {
        _spawnPoints.Clear();

        Transform spawnPoints = transform.Find("Spawn Points");
        if (spawnPoints)
        {
            foreach (Transform spawnPoint in spawnPoints)
            {
                _spawnPoints.Add(spawnPoint);
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
        SpawnKnights();
    }

    private void SpawnKnights()
    {
        foreach (var point in _spawnPoints)
        {
            SpawnKnight(point.position); 
        }
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
}
