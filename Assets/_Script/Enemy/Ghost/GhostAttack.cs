using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAttack : EnemyAttack
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Enemy start attack");
        if (collision.CompareTag("Kid"))
        {
            StartAttack();
        }
    }
    public override void StartAttack()
    {
        base.StartAttack();
        controller.Health.UpdateHealth(-100);

        controller.EnemyAnimation.TriggerAnimationDie();
    }
}
