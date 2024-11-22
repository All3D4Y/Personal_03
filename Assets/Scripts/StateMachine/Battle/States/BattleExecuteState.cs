using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattlePhase
{
    public class BattleExecuteState : IState
    {
        BattleManager battleManager;

        public BattleExecuteState(BattleManager battleManager)
        {
            this.battleManager = battleManager;
        }

        public void Enter()
        {
            // 전투단계 진입 시 실행할 코드
            // 애니메이션 재생
            Debug.Log("BattleExecute 상태 진입");
            battleManager.DamageCalculate();
        }
        public void Exit()
        {
            // 전투단계에서 종료 시 실행할 코드
            battleManager.SlotController.SortAllSlot();
            battleManager.TurnCount();
            Debug.Log("BattleExecute 상태 종료");
        }
        public void Execute()
        {
            // 전투단계 진행 중 실행할 코드
            // 한 쪽 진영 전멸 여부에 따라 BattleEnd or BattlePrep 어느 상태로 이동할지 결정
            //Debug.Log("OnBattle 상태 진행 중");
            // 애니메이션이 끝나는 시점에 전멸확인, 배틀종료 or Prep단계로

            if (battleManager.Test)
            {
                if (battleManager.SlotController.IsEliminated())
                {
                    Debug.Log("전멸한 진영 있음");
                    battleManager.BattleOver();
                }
                else
                {
                    Debug.Log("전멸한 진영 없음");
                    battleManager.Phase.ChangeState(battleManager.Phase.Prep);
                }
            }
        }
    }
}