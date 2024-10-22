using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnCalculator
{
    BattleSlot[] characterSlots;
    BattleSlot[] enemySlots;

    CharacterData[] characterDatas;
    EnemyDataBase[] enemyDatas;

    public TurnCalculator(SlotController controller)
    {
        characterSlots = controller.CharacterSlot;
        enemySlots = controller.EnemySlot;

        characterDatas = new CharacterData[characterSlots.Length];
        enemyDatas = new EnemyDataBase[enemySlots.Length];

        RefreshSlotData();
    }

    /// <summary>
    /// 슬롯의 데이터를 새로고침하는 함수
    /// </summary>
    public void RefreshSlotData()
    {
        for (int i = 0; i < characterSlots.Length; i++)
        {
            characterDatas[i] = characterSlots[i].EntityData as CharacterData;
        }
        for (int i = 0; i < enemySlots.Length; i++)
        {
            enemyDatas[i] = enemySlots[i].EntityData as EnemyDataBase;
        }
    }

    /// <summary>
    /// 캐릭터 슬롯들과 적 슬롯들 중 차례가 될 슬롯의 타입과 인덱스를 리턴하는 함수
    /// </summary>
    /// <param name="characters">캐릭터 슬롯들</param>
    /// <param name="enemies">적 슬롯들</param>
    /// <returns></returns>
    (EntityType, uint) NextTurnSlotIndex(BattleSlot[] characters, BattleSlot[] enemies)
    {
        List<BattleSlot> entities = new List<BattleSlot>();

        for (int i = 0; i < characters.Length; i++)
        {
            entities.Add(characters[i]);
        }
        for (int i = 0; i < enemies.Length; i++)
        {
            entities.Add(enemies[i]);
        }

        entities.Sort((current, other) => other.EntityData.Speed.CompareTo(current.EntityData.Speed));

        return (entities[0].Type, entities[0].Index);
    }

    public (EntityType, uint) GetTurnSlotIndex()
    {
        return NextTurnSlotIndex(characterSlots, enemySlots);
    }

    public BattleSlot NextTurnSlot()
    {
        List<BattleSlot> entities = new List<BattleSlot>();

        for (int i = 0; i < characterSlots.Length; i++)
        {
            entities.Add(characterSlots[i]);
        }
        for (int i = 0; i < enemySlots.Length; i++)
        {
            entities.Add(enemySlots[i]);
        }

        entities.Sort((current, other) => other.EntityData.Speed.CompareTo(current.EntityData.Speed));

        return entities[0];
    }
}
