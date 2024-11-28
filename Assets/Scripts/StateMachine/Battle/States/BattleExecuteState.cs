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
            battleManager.OnTurnSlot.ActorData.AttackAnimation((int)battleManager.OnTurnSlot.ActorData.Type);   // 공격 애니메이션
            

        }
        public void Exit()
        {
            // 전투단계에서 종료 시 실행할 코드
            battleManager.ClearTurnSlot();
            battleManager.SlotController.SortAllSlot();
            battleManager.TurnCount();
            Debug.Log("BattleExecute 상태 종료");
        }
        public void Execute()
        {
            // 전투단계 진행 중 실행할 코드
            //Debug.Log("OnBattle 상태 진행 중");
            // 애니메이션이 끝나는 시점에 대미지 계산

            if (battleManager.OnTurnSlot.ActorData.IsAttackEnd) // 공격 애니메이션이 끝났으면
            {
                foreach (var slot in battleManager.Targets)
                {
                    slot.ActorData.HurtAnimation();     // 피격 애니메이션
                }

                battleManager.OnTurnSlot.ActorData.IsAttackEnd = false; // 불린 초기화
            }

            if (battleManager.Targets[battleManager.Targets.Length - 1].ActorData.IsHurtEnd)    // 마지막 피격 애니메이션이 끝났으면
            {
                battleManager.DamageCalculate();    // 대미지 적용 후

                foreach (var slot in battleManager.Targets)
                {
                    slot.ActorData.IsHurtEnd = false;  // 불린 초기화 후
                }

                battleManager.Phase.ChangeState(battleManager.Phase.End);   // end페이즈로

                //if (battleManager.SlotController.IsEliminated())
                //{
                //    Debug.Log("전멸한 진영 있음");
                //    battleManager.BattleOver();
                //}
                //else
                //{
                //    Debug.Log("전멸한 진영 없음");
                //    battleManager.Phase.ChangeState(battleManager.Phase.Prep);
                //} // exitState 새로 만들어서 거기다 옮기기
            }
        }
    }
}