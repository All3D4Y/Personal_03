using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateUpdate : BattleState
{
    public StateUpdate(BattleManager manager) : base(manager) {}

    public override void Enter()
    {
        Debug.Log("상태 처리 단계...");
        // 지속 데미지, 상태 갱신
        
        manager.ChangeState<TurnEnd>();
    }

    public override void Update()
    {
    }

    public override void Exit()
    {
    }

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
