using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyChaseState : ToyState
{
    [SerializeField] private Transform _target;

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

        FindTarget();

        if (_target == null)
        {
            toyStateMachine.ChangeState(toy.IdleState);
        }

        ChaseTarget();
        if (toy.IsWithinAttackDistance)
        {
            toyStateMachine.ChangeState(toy.AttackState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void FindTarget()
    {
        GameObject[] allTargets = GameObject.FindGameObjectsWithTag("Enemy");
        if (allTargets.Length > 0)
        {
            _target = allTargets[0].transform;
            foreach (GameObject target in allTargets)
            {
                if (Vector2.Distance(toy.transform.position, target.transform.position) < Vector2.Distance(toy.transform.position, _target.transform.position))
                {
                    _target = target.transform;
                }
            }
        }
    }

    private void ChaseTarget()
    {
        Vector2 moveDirection = (_target.position - toy.transform.position).normalized;
        toy.Move(moveDirection * toy.BaseStats.MovementSpeed);
    }
}
