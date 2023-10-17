using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeAttack : EnemyAttack
{
    [SerializeField] private bool _canAttack = true;
    [SerializeField] private LayerMask _targetLaymask;
    public float AttackRange = 2;

    private void Update()
    {
        //if(Input.GetKey(KeyCode.Space))
        //{
            StartAttack();
        //}        
    }

    public override void StartAttack()
    {
        base.StartAttack();
        if(!_canAttack)
        {
            return;
        }
        Debug.Log("Snake StartAttack");
        controller.EnemyAnimation.TriggerAnimationAttack();
        _canAttack = false;
        StartCoroutine(AllowToAttack());

    }
    
    public override void EndAttack()
    {
        base.EndAttack();
        Collider2D[] toysToDamage = Physics2D.OverlapCircleAll(attackPoint.position, AttackRange, _targetLaymask);
        if(toysToDamage.Length > 0)
        {
            toysToDamage[0].GetComponent<IDamageable>().TakeDamage(2);
        }
      
    }

    private IEnumerator AllowToAttack()
    {
        yield return new WaitForSeconds(controller.Enemy.BaseStats.AttackSpeed);
        _canAttack = true;
    }
}
