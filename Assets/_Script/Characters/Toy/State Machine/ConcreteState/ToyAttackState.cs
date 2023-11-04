using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyAttackState : ToyState
{

    public ToyAttackState(Toy toy, ToyStateMachine toyStateMachine) : base(toy, toyStateMachine)
    {
        
    }

    public override void AnimationCallbackEvent(Toy.AnimationTriggerType triggerType)
    {
        base.AnimationCallbackEvent(triggerType);
        if(triggerType == Toy.AnimationTriggerType.ToyAttack)
        {
            toy.EndAttack();
        }
    }

    public override void EnterState()
    {
        base.EnterState();
        
        toy.Move(Vector2.zero);
        toy.TriggerAnimation(Toy.AnimationTriggerType.ToyIdle);
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        toy.Move(Vector2.zero);
        toy.TriggerAnimation(Toy.AnimationTriggerType.ToyAttack);
        toy.StartAttack();
        if (!toy.IsWithinAttackDistance)
        {
            Debug.Log(toy.name + " / " + toy.IsWithinAttackDistance);
            toyStateMachine.ChangeState(toy.ChaseState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
