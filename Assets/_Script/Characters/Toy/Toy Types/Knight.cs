using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Toy
{
    private bool _attack = true;
    private float _attackCircle = 0.2f;
    
    public override void StartAttack()
    {
        if(!_attack)
        {
            return;
        }
        base.StartAttack();
        TriggerAnimation(Toy.AnimationTriggerType.ToyAttack);
        _attack = false;
        StartCoroutine(AttackDelay());
    }

    public override void EndAttack()
    {
        base.EndAttack();
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(_attackPoint.position, _attackCircle, _targetLayerMask);
        if (enemiesToDamage.Length > 0)
        {
            var damageable = enemiesToDamage[0].GetComponent<IDamageable>();  // get 0 means the toy only attack single target
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
