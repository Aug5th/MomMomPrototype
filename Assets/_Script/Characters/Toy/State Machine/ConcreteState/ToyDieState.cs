using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyDieState : ToyState
{
    public ToyDieState(Toy toy, ToyStateMachine toyStateMachine) : base(toy, toyStateMachine)
    {
    }

    public override void AnimationCallbackEvent(Toy.AnimationTriggerType triggerType)
    {
        base.AnimationCallbackEvent(triggerType);
        ReleaseToy();
    }

    public override void EnterState()
    {
        base.EnterState();
        toy.TriggerAnimation(Toy.AnimationTriggerType.ToyDie);
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

    private void ReleaseToy()
    {
        toy.ReleaseToy();
    }
}
