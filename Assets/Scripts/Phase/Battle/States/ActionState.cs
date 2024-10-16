using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattlePhase
{
    public class ActionState : IState
    {
        //PhaseManager phaseManager;
        //
        //public ActionState(PhaseManager phaseManager)
        //{
        //    this.phaseManager = phaseManager;
        //}

        public void Enter()
        {
            // 행동단계 진입 시 실행할 코드
            // 턴인 캐릭터 기능 활성
            Debug.Log("Action 상태 진입");
        }
        public void Exit()
        {
            // 행동단계에서 종료 시 실행할 코드
            // 선택한 기능 BattleState로 전달
            Debug.Log("Action 상태 종료");
        }
        public void Update()
        {
            // 행동단계 진행 중 실행할 코드
            // 자리 이동, 캐릭터변경, 스킬, 아이템 선택
            Debug.Log("Action 상태 진행 중");
        }
    }
}