using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Enemy : MyMonoBehaviour, IDamageable
{
    public EnemyStats BaseStats { get; private set; }
    public EnemyType EnemyType { get; private set; }
    

    private ObjectPool<Enemy> _enemyPool;

    private Rigidbody2D _rigidbody;
    private Collider2D _collider;
    private EnemyAnimation _enemyAnimation;

    protected override void LoadComponents()
    {
        // init components here
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _enemyAnimation = GetComponentInChildren<EnemyAnimation>();
        base.LoadComponents();
    }

    public void SetStats(EnemyStats stats) => BaseStats = stats;

    public void SetType(EnemyType type) => EnemyType = type;

    public void SetPool(ObjectPool<Enemy> enemyPool) => _enemyPool = enemyPool;

    public void TakeDamage(float damage)
    {
        _enemyAnimation.TriggerAnimationDie();
    }

    public void ReleaseEnemy()
    {
        _enemyPool.Release(this);
    }

}
