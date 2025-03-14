using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 전투 결과 상태
/// </summary>
public class Conclusion : BattleState
{
    public Conclusion(BattleManager manager) : base(manager) {}
    public override void Enter()
    {
#if UNITY_EDITOR
        Debug.Log("전투 종료..."); 
#endif

        // 캐릭터 없애기
        Factory.Instance.CharacterFactory.DestroyAllCharacter();

        // 전투 결과 UI 초기화
        if (manager.TurnOrder.AllEnemiesDefeated)
        {
            // 승리
            GameManager.Instance.BattleUIManager.BattleEndUI.Initialize(StageDataManager.Instance.CurrentStage, true);
        }
        else if (manager.TurnOrder.AllPlayersDefeated)
        {
            // 패배
            GameManager.Instance.BattleUIManager.BattleEndUI.Initialize(StageDataManager.Instance.CurrentStage, false);
        }
        GameManager.Instance.BattleUIManager.OnBattleEnd();     // 전투 관련 UI 종료 처리
    }

    public override void Update()
    {
    }

    public override void Exit()
    {
    }
}

