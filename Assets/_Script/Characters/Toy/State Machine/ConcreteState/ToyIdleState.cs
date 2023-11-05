using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyIdleState : ToyState
{
    public ToyIdleState(Toy toy, ToyStateMachine toyStateMachine) : base(toy, toyStateMachine)
    {
    }

    public override void AnimationCallbackEvent(Toy.AnimationTriggerType triggerType)
    {
        base.AnimationCallbackEvent(triggerType);
    }

    public override void EnterState()
    {
        toy.TriggerAnimation(Toy.AnimationTriggerType.ToyIdle);
        toy.Move(Vector2.zero);
        base.EnterState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
