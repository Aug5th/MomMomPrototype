using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    public EnemyAttackState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
    }

    public override void AnimationCallbackEvent(Enemy.AnimationTriggerType triggerType)
    {
        base.AnimationCallbackEvent(triggerType);
        if(triggerType == Enemy.AnimationTriggerType.EnemyAttack)
        {
            enemy.EndAttack();
        }
    }

    public override void EnterState()
    {
        base.EnterState();
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
        enemy.StartAttack();
        if (!enemy.IsWithinAttackDistance)
        {
            enemyStateMachine.ChangeState(enemy.ChaseState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
