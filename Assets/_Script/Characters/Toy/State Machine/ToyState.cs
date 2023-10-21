using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyState
{
    protected Toy toy;
    protected ToyStateMachine toyStateMachine;

    public ToyState(Toy toy, ToyStateMachine toyStateMachine)
    {
        this.toy = toy;
        this.toyStateMachine = toyStateMachine;
    }

    public virtual void EnterState() { }
    public virtual void ExitState() { }
    public virtual void FrameUpdate() { }
    public virtual void PhysicsUpdate() { }
    public virtual void AnimationCallbackEvent(Toy.AnimationTriggerType triggerType) { }
}
