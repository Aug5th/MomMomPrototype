using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Rat : Enemy
{

    private bool _attack = true;
    private float _attackCircle = 0.3f;
    public override void StartAttack()
    {
        if(!_attack)
        {
            return;
        }

        base.StartAttack();
        TriggerAnimation(Enemy.AnimationTriggerType.EnemyAttack);
        StartCoroutine(AttackDelay());
        _attack = false;
    }

    public override void EndAttack()
    {
        base.EndAttack();
        Collider2D[] toysToDamage = Physics2D.OverlapCircleAll(_attackPoint.position, _attackCircle, _targetLayerMask);
        if (toysToDamage.Length > 0)
        {
            var damageable = toysToDamage[0].GetComponent<IDamageable>(); // get 0 means the enemy only attack single target
            if (damageable != null)
            {
                damageable.TakeDamage(BaseStats.Power);
            }
        }
    }

    private IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(BaseStats.AttackSpeed);
        _attack = true;
    }
}
