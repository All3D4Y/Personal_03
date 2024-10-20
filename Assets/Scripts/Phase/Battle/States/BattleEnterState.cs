using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BattlePhase
{
    public class BattleEnterState : IState
    {
        BattleManager battleManager;

        public BattleEnterState(BattleManager battleManager)
        {
            this.battleManager = battleManager;
        }

        public void Enter()
        {
            // 전투 진입단계 진입 시 실행할 코드
            // 로드 씬
            // 전투 진입 애니메이션 재생 및 UI활성화
            // SceneManager.LoadSceneAsync("TestBattle");

            // StageData 불러오기
            // 적, 캐릭터 배치
            Debug.Log("BattleEnter 상태 진입");
        }
        public void Exit()
        {
            // 전투 진입단계에서 종료 시 실행할 코드
            Debug.Log("BattleEnter 상태 종료");
        }
        public void Execute()
        {
            // 전투 진입단계 진행 중 실행할 코드
            //Debug.Log("BattleEnter 상태 진행 중");
        }
    }
}
