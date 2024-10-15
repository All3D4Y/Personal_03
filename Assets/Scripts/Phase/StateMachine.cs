using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BattlePhase;

[Serializable]
public class StateMachine
{
    public IState CurrentState { get; private set; }

    public BattleEnterState battleEnterState;
    public BattlePrepState prepState;
    public ActionState actionState;
    public OnBattleState onBattleState;
    public BattleEndState battleEndState;

    public void Initialize(IState start)
    {
        CurrentState = start;
        start.Enter();
    }

    public void TransitionTo(IState nextState)
    {
        CurrentState.Exit();
        CurrentState = nextState;
        nextState.Enter();
    }
    public void Update()
    {
        if (CurrentState != null)
        {
            CurrentState.Update();
        }
    }

    public StateMachine(PhaseManager phaseManager)
    {
        this.battleEnterState = new BattleEnterState(phaseManager);
        this.prepState = new BattlePrepState(phaseManager);
        this.actionState = new ActionState(phaseManager);
        this.onBattleState = new OnBattleState(phaseManager);
        this.battleEndState = new BattleEndState(phaseManager);
    }
}
