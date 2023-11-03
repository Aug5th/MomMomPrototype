using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranger : Toy
{
    private bool _shoot = true;
    protected override void LoadComponents()
    {
        base.LoadComponents();
    }

    public override void StartAttack()
    {
        Shoot();
    }

    private void Shoot()
    {
        if(!_shoot)
        {
            return;
        }
        // spawn bullet
        Vector2 direction = Target.position - _attackPoint.position;
        var bullet = ProjectileSpawner.Instance.SpawnBullet();
        bullet.transform.SetPositionAndRotation(_attackPoint.position, _attackPoint.rotation);
        bullet.transform.right = direction;
        bullet.GetComponent<Rigidbody2D>().velocity = direction.normalized * bullet.BaseStats.Speed;
        

        StartCoroutine(ShootDelay());
        _shoot = false;
    }

    private IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(1f);
        _shoot = true;
    }


}
