using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattlePhase
{
    public class BattlePrepState : IState
    {
        OldBattleManager battleManager;


        public BattlePrepState(OldBattleManager battleManager)
        {
            this.battleManager = battleManager;
        }

        public void Enter()
        {
            // 전투 준비단계 진입 시 실행할 코드
            // 턴 계산
            Debug.Log("BattlePrep 상태 진입");
            BattleSlot onTurn = battleManager.TurnCalculator.NextTurnSlot();
            battleManager.SetTurnSlot(onTurn);
            OldGameManager.Instance.GuideLine.SetSkill();
        }
        public void Exit() 
        {
            // 전투 준비단계 종료 시 실행할 코드
            OldGameManager.Instance.BattleUI.SkillUIs.Initialize();
            OldGameManager.Instance.BattleUI.OnSkillGroup();
            Debug.Log("BattlePrep 상태 종료");
        }
        public void Execute() 
        {
            // 전투 준비단계 진행 중 실행할 코드
            //Debug.Log("BattlePrep 상태 진행 중");
            if (battleManager.OnTurnSlot != null)
            {
                battleManager.Phase.ChangeState(battleManager.Phase.Action);    // 액션단계로
            }
        }
    }
}
