using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 버프 등 갱신 상태
/// </summary>
public class StateUpdate : BattleState
{
    public StateUpdate(BattleManager manager) : base(manager) {}

    public override void Enter()
    {
#if UNITY_EDITOR
        Debug.Log("상태 처리 단계..."); 
#endif
        
        BuffUpdate();                       // 버프 갱신
        manager.ChangeState<TurnEnd>();     // 턴 종료 단계로
    }

    public override void Update()
    {
    }

    public override void Exit()
    {
    }

    /// <summary>
    /// 버프를 갱신하는 함수
    /// </summary>
    void BuffUpdate()
    {
        foreach (Character c in manager.PlayerParty)
        {
            c.BuffManager.BuffUpdate();
        }
        foreach (Character c in manager.EnemyParty)
        {
            c.BuffManager.BuffUpdate();
        }
    }
}
