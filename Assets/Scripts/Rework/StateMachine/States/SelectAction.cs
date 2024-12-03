using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectAction : BattleState
{
    public SelectAction(BattleManager manager) : base(manager) {}

    public override void Enter()
    {
        Debug.Log("액션 선택 중...");
    }

    public override void Update()
    {
    }

    public override void Exit()
    {
    }
}
