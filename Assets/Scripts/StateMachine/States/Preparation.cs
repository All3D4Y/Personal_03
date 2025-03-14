using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 전투 준비 상태
/// </summary>
public class Preparation : BattleState
{
    public Preparation(BattleManager manager) : base(manager) {}

    public override void Enter()
    {
        // UI 초기화, 캐릭터 배치, 기타 설정
        Debug.Log("전투 준비 중...");

        // 배경 로드
        GameManager.Instance.BackGroundManager.SetBackGround(StageDataManager.Instance.CurrentStage.bg);

        // 캐릭터 배치
        manager.InitializeBattle();

        // UI 초기화
        Factory.Instance.CharacterUIPool.Initialize();
        GameManager.Instance.BattleUIManager.PreInitialize();

        // TurnOrder 초기화
        manager.TurnOrder = new TurnOrder();
        manager.TurnOrder.Initialize(manager.PlayerParty, manager.EnemyParty);
        manager.SetTurnCharacter(manager.TurnOrder.GetNextCharacter());

        // BuffManager 초기화
        foreach (Character c in manager.PlayerParty)
        {
            c.BuffManager.Initialize();
        }
        foreach (Character c in manager.EnemyParty)
        {
            c.BuffManager.Initialize();
        }

        // EnemyAction 초기화
        manager.EnemyAction = new EnemyAction();

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
