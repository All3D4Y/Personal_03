using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnCalculator
{
    BattleSlot[] characterSlots;
    BattleSlot[] enemySlots;

    CharacterData[] characterDatas;
    EnemyDataBase[] enemyDatas;

    public TurnCalculator(BattleManager battleManager)
    {
        characterSlots = battleManager.SlotController.CharacterSlot;
        enemySlots = battleManager.SlotController.EnemySlot;

        RefreshSlotData();
    }

    /// <summary>
    /// 슬롯의 데이터를 새로고침하는 함수
    /// </summary>
    public void RefreshSlotData()
    {
        characterDatas = new CharacterData[characterSlots.Length];
        enemyDatas = new EnemyDataBase[enemySlots.Length];

        for (int i = 0; i < characterSlots.Length; i++)
        {
            characterDatas[i] = characterSlots[i].EntityData as CharacterData;
        }
        for (int i = 0; i < enemySlots.Length; i++)
        {
            enemyDatas[i] = enemySlots[i].EntityData as EnemyDataBase;
        }
    }

    public BattleSlot NextTurnSlot()
    {
        List<BattleSlot> entities = new List<BattleSlot>();         // BattleSlot의 리스트를 만들고

        RefreshSlotData();

        for (int i = 0; i < characterSlots.Length; i++)
        {
            if (!characterSlots[i].IsEmpty)
            {
                entities.Add(characterSlots[i]);
            }
        }
        for (int i = 0; i < enemySlots.Length; i++)
        {
            if (!enemySlots[i].IsEmpty)
            {
                entities.Add(enemySlots[i]);
            }
        }

        entities.Sort((current, other) => current.EntityData.Speed.CompareTo(other.EntityData.Speed));

        entities.Reverse();

        return entities[0];
    }
}
