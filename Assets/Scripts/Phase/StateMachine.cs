using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class StateMachine
{
    protected IState CurrentState { get; set; }
    

    public virtual void Initialize(IState start)
    {
        CurrentState = start;
        start.Enter();
    }

    public virtual void ChangeState(IState nextState)
    {
        if (CurrentState != null)
        {
            CurrentState.Exit();
        }

        CurrentState = nextState;
        CurrentState.Enter();
    }
    public abstract void Updated();
}
