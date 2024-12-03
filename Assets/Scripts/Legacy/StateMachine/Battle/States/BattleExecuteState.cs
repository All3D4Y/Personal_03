using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattlePhase
{
    public class BattleExecuteState : IState
    {
        OldBattleManager battleManager;

        public BattleExecuteState(OldBattleManager battleManager)
        {
            this.battleManager = battleManager;
        }

        public void Enter()
        {
            // 전투단계 진입 시 실행할 코드
            // 애니메이션 재생
            Debug.Log("BattleExecute 상태 진입");
            battleManager.OnTurnSlot.ActorData.Anim.onAttackAnimEnd += () => 
            {
                foreach (var slot in battleManager.Targets)
                {
                    slot.ActorData.HurtAnimation();     // 피격 애니메이션 재생
                }
            };
            battleManager.Targets[battleManager.Targets.Length - 1].ActorData.Anim.onHurtAnimEnd += () =>
            {
                battleManager.DamageCalculate();    // 대미지 적용 후
                battleManager.Phase.ChangeState(battleManager.Phase.End);   // end페이즈로
            };
            battleManager.OnTurnSlot.ActorData.AttackAnimation((int)battleManager.OnTurnSlot.ActorData.Type);   // 공격 애니메이션


        }
        public void Exit()
        {
            // 전투단계에서 종료 시 실행할 코드
            battleManager.OnTurnSlot.ActorData.Anim.onAttackAnimEnd = null;
            battleManager.Targets[battleManager.Targets.Length - 1].ActorData.Anim.onHurtAnimEnd = null;

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