#define TestLogEnable

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

    public BattleSlot[] CharacterSlot => characterSlot;
    public BattleSlot[] EnemySlot => enemySlot;

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

    /// <summary>
    /// 슬롯에 데이터를 할당하는 함수
    /// </summary>
    /// <param name="data">넣을 데이터</param>
    /// <param name="index">넣을 슬롯의 인덱스</param>
    /// <param name="isStandbySlot">대기석인지 아닌지(true면 대기석, false면 전투석)</param>
    public void AssignSlot(EntityData data, uint index, bool isStandbySlot = false)
    {
        if (data is CharacterData)
        {
            if (!isStandbySlot)
                characterSlot[index].AssignData(data);
            else
                characterStandbySlot[index].AssignData(data);
        }
        else
        {
            if (!isStandbySlot)
                enemySlot[index].AssignData(data);
            else
                enemyStandbySlot[index].AssignData(data);
        }
    }

    /// <summary>
    /// 캐릭터와 적 데이터들을 받아와서 전투슬롯을 채우고 남은 데이터는 대기석에 채우는 함수
    /// </summary>
    /// <param name="characterDatas">캐릭터 데이터들</param>
    /// <param name="enemyDatas">적 데이터들</param>
    public void InitialAssign(CharacterData[] characterDatas, EnemyDataBase[] enemyDatas)
    {
        // 캐릭터 슬롯 채우기
        Queue<EntityData> entityQueue = new Queue<EntityData>();
        for (int i = 0; i < characterDatas.Length; i++)
        {
            entityQueue.Enqueue(characterDatas[i] as EntityData);   // 큐에 캐릭터 데이터들을 다 넣고
        }

        for (int i = 0; i < characterSlot.Length; i++)
        {
            EntityData temp = entityQueue.Dequeue();                // 큐에서 하나를 꺼내서
            characterSlot[i].AssignData(temp);                      // 캐릭터 전투 슬롯을 처음부터 끝까지 채움
        }
        int index = 0;
        while (entityQueue.Count > 0)                               // 큐에 내용물이 남아있는 동안
        {
            EntityData temp = entityQueue.Dequeue();                // 큐에서 하나를 꺼내서
            characterStandbySlot[index].AssignData(temp);           // 캐릭터 대기석 맨 앞을 채우고
            index++;                                                // 인덱스를 1 카운트 함
        }
        entityQueue.Clear();

        // 적 슬롯 채우기
        for (int i = 0; i < enemyDatas.Length; i++)
        {
            entityQueue.Enqueue(enemyDatas[i] as EntityData);       // 큐에 적 데이터들을 다 넣고
        }

        for (int i = 0; i < enemySlot.Length; i++)
        {
            EntityData temp = entityQueue.Dequeue();                // 큐에서 하나를 꺼내서
            enemySlot[i].AssignData(temp);                          // 적 전투 슬롯을 처음부터 끝까지 채움
        }
        index = 0;
        while (entityQueue.Count > 0)                               // 큐에 내용물이 남아있는 동안
        {
            EntityData temp = entityQueue.Dequeue();                // 큐에서 하나를 꺼내서
            enemyStandbySlot[index].AssignData(temp);               // 적 대기석 맨 앞을 채우고
            index++;                                                // 인덱스를 1 카운트 함
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
        slotA.AssignData(slotB.EntityData);
        slotB.AssignData(tempData);

        TestPrint();
    }

    /// <summary>
    /// 슬롯을 정렬하는 함수 (빈 슬롯이 있을 경우 0번 자리부터 순서대로 채움)
    /// </summary>
    /// <param name="slots">정렬할 슬롯들 (캐릭터, 적, 캐릭터 대기석, 적 대기석)</param>
    public void SortSlot(BattleSlot[] slots)
    {
        List<BattleSlot> entityOnField = new List<BattleSlot>(slots);
        
        entityOnField.Sort((current, other) => current.IsEmpty.CompareTo(other.IsEmpty));   // 비어있는 슬롯은 뒤로 보내기
        
        // 정렬된 데이터를 기준으로 데이터 따로 저장(직접 sortList를 사용하면 데이터가 섞이게 된다(참조를 저장하고 있기 때문에))
        List<EntityData> sortedData = new List<EntityData>(Default_Slot_Count);
        foreach (var slot in entityOnField)
        {
            sortedData.Add((slot.EntityData));
        }
        int index = 0;
        foreach (var data in sortedData)
        {
            slots[index].AssignData(data);    // 따로 저장한 내용 순서대로 슬롯에 설정
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
    /// <summary>
    /// SlotController 안의 모든 슬롯을 정렬하는 함수
    /// </summary>
    public void SortAllSlot()
    {
        SortSlot(characterSlot);
        SortSlot(enemySlot);
        SortSlot(characterStandbySlot);
        SortSlot(enemyStandbySlot);
    }

    /// <summary>
    /// 하나의 슬롯을 비우는 함수
    /// </summary>
    /// <param name="slot">비우려는 슬롯</param>
    public void ClearSlot(BattleSlot slot)
    {
        slot.ClearData();
    }
    /// <summary>
    /// 하나의 슬롯을 비우는 함수
    /// </summary>
    /// <param name="slot">비우려는 슬롯</param>
    public void ClearSlot(StandbySlot slot)
    {
        ClearSlot(slot as BattleSlot);
    }
    /// <summary>
    /// SlotController 안의 모든 슬롯을 비우는 함수
    /// </summary>
    public void ClearAllSlot()
    {
        for (int i = 0; i < Default_Slot_Count; i++)
        {
            characterSlot[i].ClearData();
            enemySlot[i].ClearData();
            characterStandbySlot[i].ClearData();
            enemyStandbySlot[i].ClearData();
        }
    }

#if UNITY_EDITOR
    public void TestPrint()
    {
        Debug.Log($"[{characterSlot[3].EntityData.entityName}] [{characterSlot[2].EntityData.entityName}] " +
            $"[{characterSlot[1].EntityData.entityName}] [{characterSlot[0].EntityData.entityName}] " +
            $"\n[{enemySlot[0].EntityData.entityName}] [{enemySlot[1].EntityData.entityName}] " +
            $"[{enemySlot[2].EntityData.entityName}] [{enemySlot[3].EntityData.entityName}]");
        
        //string printLog;
        //switch (index)
        //{
        //    case 0:
        //        for (int i = 0; i < Default_Slot_Count; i++)
        //        {
        //            printLog = characterSlot[i].IsEmpty ? "비어있음" : characterSlot[i].EntityData.name;
        //            Debug.Log($"\n캐릭터 전투 슬롯({i}) : [{printLog}]");
        //        }
        //        break;
        //    case 1:
        //        for (int i = 0; i < Default_Slot_Count; i++)
        //        {
        //            printLog = enemySlot[i].IsEmpty ? "비어있음" : enemySlot[i].EntityData.name;
        //            Debug.Log($"\n적 전투 슬롯({i}) : [{printLog}]");
        //        }
        //        break;
        //    case 2:
        //        for (int i = 0; i < Default_Slot_Count; i++)
        //        {
        //            printLog = characterStandbySlot[i].IsEmpty ? "비어있음" : characterStandbySlot[i].EntityData.name;
        //            Debug.Log($"\n캐릭터 대기 슬롯({i}) : [{printLog}]");
        //        }
        //        break;
        //    case 3:
        //        for (int i = 0; i < Default_Slot_Count; i++)
        //        {
        //            printLog = enemyStandbySlot[i].IsEmpty ? "비어있음" : enemyStandbySlot[i].EntityData.name;
        //            Debug.Log($"\n적 대기 슬롯({i}) : [{printLog}]");
        //        }
        //        break;
        //}
    }
#endif
}
