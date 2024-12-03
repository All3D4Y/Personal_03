using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preparation : BattleState
{
    public Preparation(BattleManager manager) : base(manager) {}

    public override void Enter()
    {
        // UI 초기화, 캐릭터 배치, 기타 설정
        Debug.Log("전투 준비 중...");

        // 캐릭터 배치
        //StageDataManager.Instance.CurrentStage.

        // UI 초기화


        // TurnOrder 초기화
        manager.TurnOrder = new TurnOrder();
        manager.TurnOrder.Initialize(manager.PlayerParty, manager.EnemyParty);

        // SelectAction 으로
        manager.ChangeState<SelectAction>();
    }

    public override void Update()
    {
    }

    public override void Exit()
    {
    }
}
