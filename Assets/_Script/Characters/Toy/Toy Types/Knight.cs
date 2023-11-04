using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Toy
{
    private float _attackCircle = 0.2f;
    
    public override void StartAttack()
    {
        base.StartAttack();
        if (_timer > BaseStats.AttackSpeed)
        {
            _timer = 0f;
            TriggerAnimation(Toy.AnimationTriggerType.ToyAttack);
        }
        _timer += Time.deltaTime;
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
}
