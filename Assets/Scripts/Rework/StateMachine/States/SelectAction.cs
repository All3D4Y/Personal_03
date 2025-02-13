using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SelectAction : BattleState
{
    public SelectAction(BattleManager manager) : base(manager) {}

    public override void Enter()
    {
        Debug.Log("액션 선택 중...");
        // UI 활성화
        if (manager.OnTurnCharacter.IsPlayer)
        {
            manager.OnTurnEffect.OnVisible();
            manager.OnTurnEffect.TransformUpdate();
            GameManager.Instance.BattleUIManager.Initialize();
        }   
        else
        {
            manager.EnemyAction.Initialize();
            manager.EnemyAction.OnMoveValidSlot();
        }
    }

    public override void Update()
    {
    }

    public override void Exit()
    {
        // UI 비활성화
        manager.OnTurnEffect.OnTransparent();
        GameManager.Instance.BattleUIManager.Clear();
        GameManager.Instance.BattleUIManager.OnTransparent();
    }
}
