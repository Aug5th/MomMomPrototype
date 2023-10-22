using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    public EnemyIdleState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
    }

    public override void AnimationCallbackEvent(Enemy.AnimationTriggerType triggerType)
    {
        base.AnimationCallbackEvent(triggerType);
    }

    public override void EnterState()
    {
        enemy.TriggerAnimation(Enemy.AnimationTriggerType.EnemyIdle);
        base.EnterState();
    }

    public override void ExitState()
    {
        
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        enemy.Move(Vector2.zero);
        base.FrameUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
