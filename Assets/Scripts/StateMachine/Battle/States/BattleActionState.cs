using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattlePhase
{
    public class BattleActionState : IState
    {
        BattleManager battleManager;

        public BattleActionState(BattleManager battleManager)
        {
            this.battleManager = battleManager;
        }

        public void Enter()
        {
            // 행동단계 진입 시 실행할 코드
            // 턴인 캐릭터 기능 활성
            // 델리게이트 등록
            Debug.Log("Action 상태 진입");
            if (battleManager.OnTurnSlot.Side == ActorSide.Ally)
            {
                battleManager.BattleInput.onScroll += battleManager.OnMoveSlot;

            }
        }
        public void Exit()
        {
            // 행동단계에서 종료 시 실행할 코드
            // 선택한 기능 BattleState로 전달
            // 델리게이트 해제
            if (battleManager.BattleInput.onScroll != null)
            {
                battleManager.BattleInput.onScroll -= battleManager.OnMoveSlot;
            }
            Debug.Log("Action 상태 종료");
        }
        public void Execute()
        {
            // 행동단계 진행 중 실행할 코드
            // 자리 이동, 캐릭터변경, 스킬, 아이템 선택
            // UI 구현 하면서 마무리 해야할 듯 일단은 테스트 코드로 스킬, 아이템 사용

            //Debug.Log($"{battleManager.OnTurnSlot.ActorData.ActorName}Action 상태 진행 중");
        }
    }
}