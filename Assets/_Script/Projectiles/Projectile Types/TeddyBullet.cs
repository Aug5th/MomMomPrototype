using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeddyBullet : Projectile
{
    protected override void HandleTriggerCollider(Collider2D collider)
    {
        base.HandleTriggerCollider(collider);
        if (collider.CompareTag("Enemy") || collider.CompareTag("NomNom"))
        {
            collider.GetComponent<IDamageable>().TakeDamage(BaseStats.Power);
            ReleaseProjectile();
        }
    }


}
