using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    [SerializeField] private LayerMask _targetLayerMask;
    [SerializeField] private Transform _attackPoint;
    private float _attackCircle = 0.1f;

    private float _timer;

    public EnemyAttackState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
        _targetLayerMask = LayerMask.GetMask("Toy");
        _attackPoint = enemy.transform.Find("AttackPoint");
    }

    public override void AnimationCallbackEvent(Enemy.AnimationTriggerType triggerType)
    {
        base.AnimationCallbackEvent(triggerType);
        if(triggerType == Enemy.AnimationTriggerType.EnemyAttack)
        {
            EndAttack();
        }
    }

    public override void EnterState()
    {
        base.EnterState();
        _timer = enemy.BaseStats.AttackSpeed + 1f;
        enemy.Move(Vector2.zero);
        enemy.TriggerAnimation(Enemy.AnimationTriggerType.EnemyIdle);
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        UpdateTargetLayerMark();
        StartAttack();
        if (!enemy.IsWithinAttackDistance)
        {
            enemyStateMachine.ChangeState(enemy.ChaseState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void StartAttack()
    {
        if(_timer > enemy.BaseStats.AttackSpeed)
        {
            _timer = 0f;
            Debug.Log("Snake Attack");
            enemy.TriggerAnimation(Enemy.AnimationTriggerType.EnemyAttack);
        }
        _timer += Time.deltaTime;
    }

    private void UpdateTargetLayerMark()
    {
        if(enemy.IsChasingKid)
        {
            _targetLayerMask = LayerMask.GetMask("Kid");
        }
        else
        {
            _targetLayerMask = LayerMask.GetMask("Toy");
        }
       
    }

    public void EndAttack()
    {
        Collider2D[] toysToDamage = Physics2D.OverlapCircleAll(_attackPoint.position, _attackCircle, _targetLayerMask);
        if (toysToDamage.Length > 0)
        {
            toysToDamage[0].GetComponent<IDamageable>().TakeDamage(enemy.BaseStats.Power);
        }
    }
}
