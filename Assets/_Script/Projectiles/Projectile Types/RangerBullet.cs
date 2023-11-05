using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerBullet : Projectile
{
    protected override void HandleTriggerCollider(Collider2D collider)
    {
        base.HandleTriggerCollider(collider);
        if (collider.CompareTag("Enemy"))
        {
            collider.GetComponent<IDamageable>().TakeDamage(BaseStats.Power);
            ReleaseProjectile();
        }
    }
}
