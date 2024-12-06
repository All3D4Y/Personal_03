using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnEnd : BattleState
{
    public TurnEnd(BattleManager manager) : base(manager) {}

    public override void Enter()
    {
        Debug.Log("턴 종료 단계...");

        TurnCount();
    }

    public override void Update()
    {
    }

    public override void Exit()
    {
    }

    void TurnCount()
    {
        manager.TurnOrder.Initialize(manager.PlayerParty, manager.EnemyParty);      // 죽어서 턴계산에서 빠진 캐릭터들을 적용

        if (!manager.TurnOrder.IsBattleOver())                                      // 배틀이 끝나지 않았으면
        {
            manager.SetTurnCharacter(manager.TurnOrder.GetNextCharacter());         // 다음 차례를 설정 하고
            manager.ChangeState<SelectAction>();                                    // 액션 선택 상태로
        }
        else                                                                        // 배틀이 끝났으면
        {                                                                       
            manager.ChangeState<Conclusion>();                                      // 결산 상태로
        }
    }
}
