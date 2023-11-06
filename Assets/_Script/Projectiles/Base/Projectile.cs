using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Projectile : MyMonoBehaviour
{
    #region Variables
    public ProjectileStats BaseStats { get; private set; }
    public ProjectileType ProjectileType { get; private set; }

    private ObjectPool<Projectile> _projectilePool;
    #endregion

    #region Initialization
    protected override void LoadComponents()
    {
        base.LoadComponents();
    }
    public void SetType(ProjectileType type) => ProjectileType = type;

    public void SetStats(ProjectileStats stats) => BaseStats = stats;

    public void SetPool(ObjectPool<Projectile> projectilePool) => _projectilePool = projectilePool;

    #endregion

    #region Damageable
    private void OnTriggerEnter2D(Collider2D collision)
    {
        HandleTriggerCollider(collision);
    }
   
    public void ReleaseProjectile()
    {
        _projectilePool.Release(this);
    }

    protected virtual void HandleTriggerCollider(Collider2D collider) 
    {
        if(collider.CompareTag("CollisionObjects"))
        {
            ReleaseProjectile();
        }
    }
    #endregion
}
