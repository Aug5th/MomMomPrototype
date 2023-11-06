
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ProjectileSpawner : Singleton<ProjectileSpawner>
{
    private ObjectPool<Projectile> _bulletPool;
    private ObjectPool<Projectile> _teddyBulletPool;
    private ObjectPool<Projectile> _poisonBulletPool;
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
        InitPoisonBulletPool();
        InitTeddyBulletPool();
        InitBulletPool();
    }

    private void InitPoisonBulletPool()
    {
        _poisonBulletPool = new ObjectPool<Projectile>(() =>
        {
            var bulletScript = ResourceSystem.Instance.GetProjectile(ProjectileType.PoisonBullet);
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
        }, false, 50, 100);
    }

    private void InitTeddyBulletPool()
    {
        _teddyBulletPool = new ObjectPool<Projectile>(() =>
        {
            var bulletScript = ResourceSystem.Instance.GetProjectile(ProjectileType.TeddyBullet);
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
        }, false, 50, 100);
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
        }, false, 50, 100);
    }

    public Projectile SpawnBullet()
    {
        var bullet = _bulletPool.Get();
        var bulletScript = ResourceSystem.Instance.GetProjectile(ProjectileType.Bullet);
        bullet.SetStats(bulletScript.BaseStats);
        bullet.SetType(bulletScript.ProjectileType);
        bullet.SetPool(_bulletPool);
        bullet.transform.SetParent(_holder);
        return bullet;
    }

    public Projectile SpawnTeddyBullet()
    {
        var bullet = _teddyBulletPool.Get();
        var bulletScript = ResourceSystem.Instance.GetProjectile(ProjectileType.TeddyBullet);
        bullet.SetStats(bulletScript.BaseStats);
        bullet.SetType(bulletScript.ProjectileType);
        bullet.SetPool(_teddyBulletPool);
        bullet.transform.SetParent(_holder);
        return bullet;
    }

    public Projectile SpawnPoisonBullet()
    {
        var bullet = _poisonBulletPool.Get();
        var bulletScript = ResourceSystem.Instance.GetProjectile(ProjectileType.PoisonBullet);
        bullet.SetStats(bulletScript.BaseStats);
        bullet.SetType(bulletScript.ProjectileType);
        bullet.SetPool(_poisonBulletPool);
        bullet.transform.SetParent(_holder);
        return bullet;
    }
}
