using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy
{
    private bool _attack = true;
    public override void StartAttack()
    {
        if (!_attack)
        {
            return;
        }
        base.StartAttack();

        
        // spawn bullet
        Vector2 direction = Target.position - _attackPoint.position;
        var bullet = ProjectileSpawner.Instance.SpawnPoisonBullet();
        bullet.transform.SetPositionAndRotation(_attackPoint.position, _attackPoint.rotation);
        bullet.transform.right = direction;
        bullet.GetComponent<Rigidbody2D>().velocity = direction.normalized * bullet.BaseStats.Speed;

        StartCoroutine(AttackDelay());
        _attack = false;
    }

    private IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(BaseStats.AttackSpeed);
        _attack = true;
    }
}
