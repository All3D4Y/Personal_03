using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattlePhase
{
    public class BattlePrepState : IState
    {
        BattleManager battleManager;


        public BattlePrepState(BattleManager battleManager)
        {
            this.battleManager = battleManager;
        }

        public void Enter()
        {
            // 전투 준비단계 진입 시 실행할 코드
            // 턴 계산
            battleManager.OnTurn();
            Debug.Log("BattlePrep 상태 진입");
        }
        public void Exit() 
        {
            // 전투 준비단계 종료 시 실행할 코드
            Debug.Log("BattlePrep 상태 종료");
        }
        public void Execute() 
        {
            // 전투 준비단계 진행 중 실행할 코드
            Debug.Log("BattlePrep 상태 진행 중");
        }
    }
}
