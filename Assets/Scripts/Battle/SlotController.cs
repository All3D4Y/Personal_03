using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotController
{
    const uint Default_Slot_Size = 4;

    BattleSlot[] characterSlot;
    BattleSlot[] enemySlot;

    BenchSlot[] characterBench;
    BenchSlot[] enemyBench;

    CharacterDataManager characterDataManager;
    EnemyDataManager enemyDataManager;

    /// <summary>
    /// SlotController 생성자
    /// </summary>
    /// <param name="size"></param>
    public SlotController(uint size = Default_Slot_Size)
    {
        characterSlot = new BattleSlot[size];
        for (uint i = 0; i < characterSlot.Length; i++)
        {
            characterSlot[i] = new BattleSlot(i);
        }
        
        enemySlot = new BattleSlot[size];
        for (uint i = 0; i < enemySlot.Length; i++)
        {
            enemySlot[i] = new BattleSlot(i);
        }

        characterBench = new BenchSlot[size];
        for (uint i = 0; i < characterBench.Length; i++)
        {
            characterBench[i] = new BenchSlot(i);
        }

        enemyBench = new BenchSlot[size];
        for (uint i = 0; i < enemyBench.Length; i++)
        {
            enemyBench[i] = new BenchSlot(i);
        }
    }

    /// <summary>
    /// 슬롯간에 스왑을 하는 함수
    /// </summary>
    /// <param name="slotA">대상1</param>
    /// <param name="slotB">대상2</param>
    public void SwapSlot(BattleSlot slotA, BattleSlot slotB)
    {
        EntityData tempData = slotA.EntityData;
        bool tempEmpty = slotA.IsEmpty;
        slotA.AssignSlot(slotB.EntityData, slotB.IsEmpty);
        slotB.AssignSlot(tempData, tempEmpty);
    }

    public void SortSlot()
    {
        List<BattleSlot> characterOnField = new List<BattleSlot>(characterSlot);
        List<BattleSlot> enemyOnField = new List<BattleSlot>(enemySlot);
        List<BenchSlot> characterOffField = new List<BenchSlot>(characterBench);
        List<BenchSlot> enemyOffField = new List<BenchSlot>(enemyBench);

        characterOnField.Sort((current, other) =>
        {
            if (current.IsEmpty) return 1;
            if (other.IsEmpty) return -1;
        });
    }
}
