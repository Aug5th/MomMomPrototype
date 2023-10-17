using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Toy : MyMonoBehaviour
{
    public ToyStats BaseStats { get; private set; }
    public ToyType ToyType { get; private set; }

    private ObjectPool<Toy> _toyPool;

    protected Rigidbody2D _rigidbody;
    protected Collider2D _collider;
    protected ToyController _controller;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _controller = GetComponent<ToyController>();
    }

    public void SetHealth()
    {
        _controller.Health.SetHealth(BaseStats.HealthPoint);
    }

    public void SetStats(ToyStats stats) => BaseStats = stats;

    public void SetType(ToyType type) => ToyType = type;

    public void SetPool(ObjectPool<Toy> toyPool) => _toyPool = toyPool;

    public void ReleaseEnemy()
    {
        _toyPool.Release(this);
    }
}
