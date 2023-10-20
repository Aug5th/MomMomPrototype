using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Toy : MyMonoBehaviour, IDamageable
{
    public ToyStats BaseStats { get; private set; }
    public ToyType ToyType { get; private set; }
    public float CurrentHealth { get; set; }
    public float MaxHealth { get; set; }

    private ObjectPool<Toy> _toyPool;

    protected Rigidbody2D _rigidbody;
    protected Collider2D _collider;
    private HealthBar _healthBar;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _healthBar = GetComponentInChildren<HealthBar>();
    }

    public void SetStats(ToyStats stats) => BaseStats = stats;

    public void SetType(ToyType type) => ToyType = type;

    public void SetPool(ObjectPool<Toy> toyPool) => _toyPool = toyPool;

    public void ReleaseToy()
    {
        _toyPool.Release(this);
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, MaxHealth);
        UpdateHealthBar();
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        //_controller.ToyAnimation.TriggerAnimationDie();
    }

    public void UpdateHealthBar()
    {
        _healthBar.UpdateHealthBar(CurrentHealth, MaxHealth);
}
}
