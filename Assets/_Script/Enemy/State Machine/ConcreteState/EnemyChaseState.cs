using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : EnemyState
{
    [SerializeField] private Transform _target;

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
        FindTarget();
        ChaseTarget();
        if (enemy.IsWithinAttackDistance)
        {
            enemyStateMachine.ChangeState(enemy.AttackState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void FindTarget()
    {
        GameObject[] allTargets = GameObject.FindGameObjectsWithTag("Toy");
        if (allTargets.Length > 0)
        {
            _target = allTargets[0].transform;
            foreach (GameObject target in allTargets)
            {
                if (Vector2.Distance(enemy.transform.position, target.transform.position) < Vector2.Distance(enemy.transform.position, _target.transform.position))
                {
                    _target = target.transform;
                }
            }
        }
    }

    private void ChaseTarget()
    {
        Vector2 moveDirection = (_target.position - enemy.transform.position).normalized;
        enemy.Move(moveDirection * enemy.BaseStats.MovementSpeed);
    }
}
