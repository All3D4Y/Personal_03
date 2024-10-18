using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotController
{
    const int Default_Slot_Count = 4;

    BattleSlot[] characterSlot;
    BattleSlot[] enemySlot;

    StandbySlot[] characterStandbySlot;
    StandbySlot[] enemyStandbySlot;

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

        characterStandbySlot = new StandbySlot[size];
        for (uint i = 0; i < characterStandbySlot.Length; i++)
        {
            characterStandbySlot[i] = new StandbySlot(EntityType.Charater, i);
        }

        enemyStandbySlot = new StandbySlot[size];
        for (uint i = 0; i < enemyStandbySlot.Length; i++)
        {
            enemyStandbySlot[i] = new StandbySlot(EntityType.Enemy, i);
        }
    }

    public void InitialAssign(CharacterData[] characterDatas, EnemyDataBase[] enemyDatas)
    {
        Queue<CharacterData> characterQueue = new Queue<CharacterData>();   
        for (int i = 0; i < characterDatas.Length; i++)
        {
            characterQueue.Enqueue(characterDatas[i]);      // 큐에 캐릭터 데이터들을 다 넣고
        }

        for (int i = 0; i < characterSlot.Length; i++)
        {
            CharacterData temp = characterQueue.Dequeue();  // 큐에서 하나를 꺼내서
            characterSlot[i].AssignSlot(temp);              // 캐릭터 슬롯을 처음부터 채움
        }
        int index = 0;
        while (characterQueue.Count > 0)                    // 큐에 내용물이 남아있는 동안
        {
            CharacterData temp = characterQueue.Dequeue();  // 큐에서 하나를 꺼내서
            characterStandbySlot[index].AssignSlot(temp);   // 캐릭터 대기석 맨 앞을 채우고
            index++;                                        // 인덱스를 1 카운트 함
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
        slotA.AssignSlot(slotB.EntityData);
        slotB.AssignSlot(tempData);
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
        List<EntityData> sortedData = new List<EntityData>(Default_Slot_Count);
        foreach (var slot in entityOnField)
        {
            sortedData.Add((slot.EntityData));
        }
        int index = 0;
        foreach (var data in sortedData)
        {
            slots[index].AssignSlot(data);    // 따로 저장한 내용 순서대로 슬롯에 설정
            index++;
        }
    }
    /// <summary>
    /// 슬롯을 정렬하는 함수 (빈 슬롯이 있을 경우 0번 자리부터 순서대로 채움)
    /// </summary>
    /// <param name="slots">정렬할 슬롯들 (캐릭터, 적, 캐릭터 대기석, 적 대기석)</param>
    public void SortSlot(StandbySlot[] slots)
    {
        SortSlot(slots as BattleSlot[]);
    }
}
