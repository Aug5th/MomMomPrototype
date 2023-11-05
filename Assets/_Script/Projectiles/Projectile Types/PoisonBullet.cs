using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonBullet : Projectile
{
    protected override void HandleTriggerCollider(Collider2D collider)
    {
        if (collider.CompareTag("Toy") || collider.CompareTag("Kid"))
        {
            collider.GetComponent<IDamageable>().TakeDamage(BaseStats.Power);
            ReleaseProjectile();
        }
    }
}
