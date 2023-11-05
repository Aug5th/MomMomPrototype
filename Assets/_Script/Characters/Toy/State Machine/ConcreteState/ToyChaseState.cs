using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyChaseState : ToyState
{
    [SerializeField] private Transform _target;
    public bool reachedEndOfPath = false;
    public ToyChaseState(Toy toy, ToyStateMachine toyStateMachine) : base(toy, toyStateMachine)
    {

    }

    public override void AnimationCallbackEvent(Toy.AnimationTriggerType triggerType)
    {
        base.AnimationCallbackEvent(triggerType);
    }

    public override void EnterState()
    { 
        base.EnterState();
        toy.TriggerAnimation(Toy.AnimationTriggerType.ToyChase);
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        if(toy.IsInHealingZone)
        {
            toyStateMachine.ChangeState(toy.IdleState);
        }
        
        if (toy.IsWithinAttackDistance && !toy.IsHealingMode)
        {
            toyStateMachine.ChangeState(toy.AttackState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        ChaseTarget();
    }

    private void ChaseTarget()
    {
        if (toy.PathMap == null)
        {
            return;
        }
        if(toy.IsHealingMode)
        {
            if(toy.IsInHealingZone)
            {
                reachedEndOfPath = true;
                return;
            }
            else
            {
                reachedEndOfPath = false;
            }
        }
        else if (toy.CurrentWayPoint >= toy.PathMap.vectorPath.Count || toy.IsWithinAttackDistance)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }
        Vector2 direction = ((Vector2)toy.PathMap.vectorPath[toy.CurrentWayPoint] - toy.Rigidbody.position).normalized;

        Vector2 force = direction * toy.BaseStats.MovementSpeed;

        toy.Move(force);


        float distance = Vector2.Distance(toy.Rigidbody.position, (Vector2)toy.PathMap.vectorPath[toy.CurrentWayPoint]);

        if (distance <= 0.032f)
        {
            toy.CurrentWayPoint++;
        }
    }
}
