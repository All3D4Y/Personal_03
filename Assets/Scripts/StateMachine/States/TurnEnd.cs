using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 턴이 종료 상태
/// </summary>
public class TurnEnd : BattleState
{
    public TurnEnd(BattleManager manager) : base(manager) {}

    public override void Enter()
    {
#if UNITY_EDITOR
        Debug.Log("턴 종료 단계..."); 
#endif

        manager.PlayerSlot.ReorderSlots();  // 플레이어 슬롯 재정렬
        manager.EnemySlot.ReorderSlots();   // 적 슬롯 재정렬
        TurnCount();                        // 다음 차례
    }

    public override void Update()
    {
    }

    public override void Exit()
    {
    }

    /// <summary>
    /// 다음 차례로 넘어가는 함수
    /// </summary>
    void TurnCount()
    {
        manager.TurnOrder.ListUpdate();                                                     // 죽어서 리스트에서 빠진 캐릭터들을 적용하는 초기화
        manager.TurnOrder.onTurnCount?.Invoke();

        if (!manager.TurnOrder.IsBattleOver())                                              // 전투가 끝나지 않았으면
        {
            manager.SetTurnCharacter(manager.TurnOrder.GetNextCharacter());                 // 다음 차례를 설정 하고
            GameManager.Instance.CoroutineManager.OnChangeState(1, typeof(SelectAction));   // 1초 후 행동 선택 상태로
        }
        else                                                                                // 전투가 끝났으면
        {                                                                       
            manager.ChangeState<Conclusion>();                                              // 결산 상태로
        }
    }
}
