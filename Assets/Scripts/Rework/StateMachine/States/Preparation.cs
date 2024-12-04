using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preparation : BattleState
{
    public Preparation(BattleManager manager) : base(manager) {}

    public override void Enter()
    {
        // UI 초기화, 캐릭터 배치, 기타 설정
        Debug.Log("전투 준비 중...");

        // Core Scripts Initialize
        StageDataManager.Instance.CurrentStage = StageDataManager.Instance.stageDatas[0];
        Debug.LogWarning("Test용 코드로 스테이지 데이터 할당 중, 수정 필요!");

        // 캐릭터 배치
        InitializeBattle();

        // UI 초기화


        // TurnOrder 초기화
        manager.TurnOrder = new TurnOrder();
        //manager.TurnOrder.Initialize(manager.PlayerParty, manager.EnemyParty);

        // SelectAction 으로
        manager.ChangeState<SelectAction>();
    }

    public override void Update()
    {
    }

    public override void Exit()
    {
    }

    void InitializeBattle()
    {
        // 슬롯 초기화
        manager.PlayerSlot = new SlotManager(8, true);              // 아군 슬롯 8개
        manager.EnemySlot = new SlotManager(8, false);              // 적 슬롯 8개

        // 아군 배치
        int[] characterCodes = PlayerDataManager.Instance.players;
        CharacterFactory CF = Factory.Instance.CharacterFactory;
        for (int i = 0; i < characterCodes.Length; i++)
        {
            Character temp = CF.GenerateCharacter(characterCodes[i], CF.transform);
            if (temp != null)
            {
                manager.PlayerSlot.AssignCharacterToSlot(temp, i);
                temp.transform.Translate(manager.PlayerSlot.GetSlot(i).SlotTransform.position);
            }
        }

        // 적 배치
        characterCodes = StageDataManager.Instance.CurrentStage.enemyCodes;
        for (int i = 0; i < characterCodes.Length; i++)
        {
            Character temp = CF.GenerateCharacter(characterCodes[i], CF.transform);
            if (temp != null)
            {
                manager.EnemySlot.AssignCharacterToSlot(temp, i);
                temp.transform.Translate(manager.EnemySlot.GetSlot(i).SlotTransform.position);
            }
        }
    }
}
