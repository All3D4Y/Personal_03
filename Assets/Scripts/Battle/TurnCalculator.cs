using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnCalculator
{
    BattleSlot[] characterSlot;
    BattleSlot[] enemySlot;

    CharacterData[] characterDatas;
    EnemyDataBase[] enemyDatas;

    public TurnCalculator(SlotController controller)
    {
        characterSlot = controller.CharacterSlot;
        enemySlot = controller.EnemySlot;

        characterDatas = new CharacterData[characterSlot.Length];
        enemyDatas = new EnemyDataBase[enemySlot.Length];

        RefreshSlotData();
    }

    public void RefreshSlotData()
    {
        for (int i = 0; i < characterSlot.Length; i++)
        {
            characterDatas[i] = characterSlot[i].EntityData as CharacterData;
        }
        for (int i = 0; i < enemySlot.Length; i++)
        {
            enemyDatas[i] = enemySlot[i].EntityData as EnemyDataBase;
        }
    }

    BattleSlot NextTurnSlotIndex(BattleSlot[] characters, BattleSlot[] enemies)
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

        entities.Sort((current, other) => current.EntityData.Speed.CompareTo(other.EntityData.Speed));

        return entities[0];
    }

    public BattleSlot GetTurnSlot()
    {
        return NextTurnSlotIndex(characterSlot, enemySlot);
    }
}
