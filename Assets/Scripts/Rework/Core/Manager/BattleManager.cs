using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    BattleState currentState;               // 현재 상태
    Dictionary<Type, BattleState> states;   // 상태 목록

    List<Character> playerParty;
    List<Character> enemyParty;

    public TurnOrder TurnOrder { get; set; }
    public SlotManager PlayerSlot { get; set; }
    public SlotManager EnemySlot { get; set; }
    public List<Character> PlayerParty => playerParty;
    public List<Character> EnemyParty => enemyParty;

    void Start()
    {
        // 상태 초기화
        states = new Dictionary<Type, BattleState>
        {
            { typeof(Preparation), new Preparation(this) },
            { typeof(SelectAction), new SelectAction(this) },
            { typeof(Execution), new Execution(this) },
            { typeof(StateUpdate), new StateUpdate(this) },
            { typeof(TurnEnd), new TurnEnd(this) },
            { typeof(Conclusion), new Conclusion(this) }
        };

        // 초기 상태 = Preparation
        ChangeState<Preparation>();
    }

    void Update()
    {
        currentState?.Update();
    }

    public void ChangeState<T>() where T : BattleState
    {
        if (currentState != null)
            currentState.Exit();

        currentState = states[typeof(T)];
        currentState.Enter();
    }
}
