using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BattlePhase
{
    public class BattleEnterState : IState
    {
        OldBattleManager battleManager;

        public BattleEnterState(OldBattleManager battleManager)
        {
            this.battleManager = battleManager;
        }

        public void Enter()
        {
            // 전투 진입단계 진입 시 실행할 코드
            // 로드 씬
            // 전투 진입 애니메이션 재생 및 UI활성화
            // SceneManager.LoadSceneAsync("TestBattle");

            Debug.Log("BattleEnter 상태 진입");
            // StageData 불러오기
            OldGameManager.Instance.StageDataManager.LoadStage(0); //test
            OldGameManager.Instance.BattleUI.Initialize();
            OldGameManager.Instance.GuideLine.Initialize();
            // visualize
            // 적, 캐릭터 배치
            // 할 거 다 하고  
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
            if (!battleManager.SlotController.AllySlot[0].IsEmpty && !battleManager.SlotController.EnemySlot[0].IsEmpty)
            {
                battleManager.Phase.ChangeState(battleManager.Phase.Prep);  // 준비단계로
            }
        }
    }
}
