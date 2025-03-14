using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 행동 선택 상태
/// </summary>
public class SelectAction : BattleState
{
    public SelectAction(BattleManager manager) : base(manager) {}

    public override void Enter()
    {
#if UNITY_EDITOR
        Debug.Log("액션 선택 중..."); 
#endif

        if (manager.OnTurnCharacter.IsPlayer)                   // 플레이어 차례일 때 UI 활성화
        {                                                       
            manager.OnTurnEffect.OnVisible();                   
            manager.OnTurnEffect.TransformUpdate();             
            GameManager.Instance.BattleUIManager.Initialize();  
        }                                                       
        else                                                    // 적 차례일 때엔 로직에 따라 적 행동 실행을 저장
        {
            manager.EnemyAction.Initialize();
            manager.EnemyAction.OnMoveValidSlot();
        }
    }

    public override void Update()
    {
    }

    public override void Exit()
    {
        // UI 비활성화
        manager.OnTurnEffect.OnTransparent();
        GameManager.Instance.BattleUIManager.Clear();
        GameManager.Instance.BattleUIManager.OnTransparent();
    }
}
