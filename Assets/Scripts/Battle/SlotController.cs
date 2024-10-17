using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotController
{
    const int Default_Slot_Count = 4;

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
    public SlotController(uint size = Default_Slot_Count)
    {
        characterSlot = new BattleSlot[size];
        for (uint i = 0; i < characterSlot.Length; i++)
        {
            characterSlot[i] = new BattleSlot(EntityType.Charater, i);
        }
        
        enemySlot = new BattleSlot[size];
        for (uint i = 0; i < enemySlot.Length; i++)
        {
            enemySlot[i] = new BattleSlot(EntityType.Enemy, i);
        }

        characterBench = new BenchSlot[size];
        for (uint i = 0; i < characterBench.Length; i++)
        {
            characterBench[i] = new BenchSlot(EntityType.Charater, i);
        }

        enemyBench = new BenchSlot[size];
        for (uint i = 0; i < enemyBench.Length; i++)
        {
            enemyBench[i] = new BenchSlot(EntityType.Enemy, i);
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

    /// <summary>
    /// 슬롯을 정렬하는 함수 (빈 슬롯이 있을 경우 0번 자리부터 순서대로 채움)
    /// </summary>
    /// <param name="slots">정렬할 슬롯들 (캐릭터, 적, 캐릭터 대기석, 적 대기석)</param>
    public void SortSlot(BattleSlot[] slots)
    {
        List<BattleSlot> entityOnField = new List<BattleSlot>(slots);
        
        entityOnField.Sort((current, other) =>
        {
            if (current.IsEmpty) return 1;      // 비어있는 슬롯은 뒤로 보내기
            if (other.IsEmpty) return -1;
            return current.Index.CompareTo(other);
        });
        
        // 정렬된 데이터를 기준으로 데이터 따로 저장(직접 sortList를 사용하면 데이터가 섞이게 된다(참조를 저장하고 있기 때문에))
        List<(EntityData, bool)> sortedData = new List<(EntityData, bool)>(Default_Slot_Count);
        foreach (var slot in entityOnField)
        {
            sortedData.Add((slot.EntityData, slot.IsEmpty));
        }
        int index = 0;
        foreach (var data in sortedData)
        {
            slots[index].AssignSlot(data.Item1, data.Item2);    // 따로 저장한 내용 순서대로 슬롯에 설정
            index++;
        }
    }
    /// <summary>
    /// 슬롯을 정렬하는 함수 (빈 슬롯이 있을 경우 0번 자리부터 순서대로 채움)
    /// </summary>
    /// <param name="slots">정렬할 슬롯들 (캐릭터, 적, 캐릭터 대기석, 적 대기석)</param>
    public void SortSlot(BenchSlot[] slots)
    {
        SortSlot(slots as BattleSlot[]);
    }
}
