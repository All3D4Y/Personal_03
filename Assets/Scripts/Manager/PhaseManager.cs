using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseManager : MonoBehaviour
{
    BattlePhaseEnum phaseState;
    StateMachine stateMachine;

    void Awake()
    {
        stateMachine = new StateMachine(this);
    }
    void Start()
    {
        stateMachine.Initialize(stateMachine.battleEnterState);
    }


}
