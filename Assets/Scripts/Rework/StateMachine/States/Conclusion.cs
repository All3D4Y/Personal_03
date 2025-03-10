﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conclusion : BattleState
{
    public Conclusion(BattleManager manager) : base(manager) {}
    public override void Enter()
    {
        Debug.Log("전투 종료...");
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
        GameManager.Instance.BattleUIManager.OnBattleEnd();
    }

    public override void Update()
    {
    }

    public override void Exit()
    {
    }
}

