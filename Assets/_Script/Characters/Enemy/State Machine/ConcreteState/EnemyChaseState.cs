using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : EnemyState
{
    public EnemyChaseState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {

    }

    public override void AnimationCallbackEvent(Enemy.AnimationTriggerType triggerType)
    {
        base.AnimationCallbackEvent(triggerType);
    }

    public override void EnterState()
    { 
        base.EnterState();
        enemy.TriggerAnimation(Enemy.AnimationTriggerType.EnemyChase);
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        ChaseTarget();
        if (enemy.IsWithinAttackDistance)
        {
            enemyStateMachine.ChangeState(enemy.AttackState);
        }

        if(!enemy.IsHavingTarget)
        {
            enemyStateMachine.ChangeState(enemy.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void ChaseTarget()
    {
        Vector2 moveDirection = (enemy.Target.position - enemy.transform.position).normalized;
        enemy.Move(moveDirection * enemy.BaseStats.MovementSpeed);
    }
}
