using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnCalculator
{
    BattleSlot[] allySlots;
    BattleSlot[] enemySlots;

    public TurnCalculator(BattleManager battleManager)
    {
        allySlots = battleManager.SlotController.AllySlot;
        enemySlots = battleManager.SlotController.EnemySlot;
    }

    /// <summary>
    /// 슬롯의 데이터를 새로고침하는 함수
    /// </summary>
    public void RefreshSlotData()
    {
        allySlots = GameManager.Instance.BattleManager.SlotController.AllySlot;
        enemySlots = GameManager.Instance.BattleManager.SlotController.EnemySlot;
    }

    public BattleSlot NextTurnSlot()
    {
        List<BattleSlot> entities = new List<BattleSlot>();         // BattleSlot의 리스트를 만들고

        RefreshSlotData();

        for (int i = 0; i < allySlots.Length; i++)
        {
            if (!allySlots[i].IsEmpty)
            {
                entities.Add(allySlots[i]);
            }
        }
        for (int i = 0; i < enemySlots.Length; i++)
        {
            if (!enemySlots[i].IsEmpty)
            {
                entities.Add(enemySlots[i]);
            }
        }

        entities.Sort((current, other) => other.ActorData.Speed.CompareTo(current.ActorData.Speed));

        return entities[0];
    }
}
