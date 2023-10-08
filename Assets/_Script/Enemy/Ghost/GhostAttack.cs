using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAttack : EnemyAttack
{
    protected override void Attack()
    {
        base.Attack();
        _controller.Health.UpdateHealth(-100);

        _controller.EnemyAnimation.TriggerAnimationDie();
    }
}
