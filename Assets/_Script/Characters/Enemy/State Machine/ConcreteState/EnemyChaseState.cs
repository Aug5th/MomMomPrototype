using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : EnemyState
{
    public bool reachedEndOfPath;
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        ChaseTarget();
    }

    private void ChaseTarget()
    {
        if (enemy.PathMap == null)
        {
            return;
        }
        if (enemy.CurrentWayPoint >= enemy.PathMap.vectorPath.Count || enemy.IsWithinAttackDistance)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }
        Vector2 direction = ((Vector2)enemy.PathMap.vectorPath[enemy.CurrentWayPoint] - enemy.Rigidbody.position).normalized;

        Vector2 force = direction * enemy.BaseStats.MovementSpeed;

        enemy.Move(force);


        float distance = Vector2.Distance(enemy.Rigidbody.position, (Vector2)enemy.PathMap.vectorPath[enemy.CurrentWayPoint]);

        if (distance <= 0.032f)
        {
            enemy.CurrentWayPoint++;
        }
    }
}
