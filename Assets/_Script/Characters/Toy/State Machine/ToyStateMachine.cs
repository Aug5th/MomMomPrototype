using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyStateMachine
{
    public ToyState CurrentToyState { get; set; }

    public void Initialize(ToyState startingState)
    {
        CurrentToyState = startingState;
        CurrentToyState.EnterState();
    }

    public void ChangeState(ToyState newState)
    {
        CurrentToyState.ExitState();
        CurrentToyState = newState;
        CurrentToyState.EnterState();
    }
}
