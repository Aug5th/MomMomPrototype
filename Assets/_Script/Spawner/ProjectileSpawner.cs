
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ProjectileSpawner : Singleton<ProjectileSpawner>
{
    private ObjectPool<Projectile> _bulletPool;
    private Transform _holder;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadProjectileHolder();
    }

    private void LoadProjectileHolder()
    {
        if (_holder != null)
        {
            return;
        }
        _holder = transform.Find("Holder");
    }

    private void Start()
    {
        InitBulletPool();
    }

    private void InitBulletPool()
    {
        _bulletPool = new ObjectPool<Projectile>(() =>
        {
            var bulletScript = ResourceSystem.Instance.GetProjectile(ProjectileType.Bullet);
            return Instantiate(bulletScript.Prefab);
        }, bullet =>
        {
            bullet.gameObject.SetActive(true);
        }, bullet =>
        {
            bullet.gameObject.SetActive(false);
        }, bullet =>
        {
            Destroy(bullet.gameObject);
        }, false, 30, 50);
    }

    public Projectile SpawnBullet()
    {
        var bullet = _bulletPool.Get();
        var arrowScript = ResourceSystem.Instance.GetProjectile(ProjectileType.Bullet);
        bullet.SetStats(arrowScript.BaseStats);
        bullet.SetType(arrowScript.ProjectileType);
        bullet.SetPool(_bulletPool);
        bullet.transform.SetParent(_holder);
        return bullet;
    }
}
