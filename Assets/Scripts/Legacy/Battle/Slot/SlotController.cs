#define TestLogEnable

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotController
{
    const int Default_Slot_Count = 4;

    BattleSlot[] allySlot;
    BattleSlot[] enemySlot;

    StandbySlot[] allyStandbySlot;
    StandbySlot[] enemyStandbySlot;

    AllyDataManager allyManager;
    EnemyDataManager enemyDataManager;

    // Properties
    public BattleSlot[] AllySlot => allySlot;
    public StandbySlot[] AllyStandbySlot => allyStandbySlot;
    public BattleSlot[] EnemySlot => enemySlot;
    public StandbySlot[] EnemyStandbySlot => enemyStandbySlot;

    /// <summary>
    /// SlotController 생성자
    /// </summary>
    /// <param name="size"></param>
    public SlotController(uint size = Default_Slot_Count)
    {
        allySlot = new BattleSlot[size];
        for (uint i = 0; i < allySlot.Length; i++)
        {
            allySlot[i] = new BattleSlot(ActorSide.Ally, i);
        }
        
        enemySlot = new BattleSlot[size];
        for (uint i = 0; i < enemySlot.Length; i++)
        {
            enemySlot[i] = new BattleSlot(ActorSide.Enemy, i);
        }

        allyStandbySlot = new StandbySlot[size];
        for (uint i = 0; i < allyStandbySlot.Length; i++)
        {
            allyStandbySlot[i] = new StandbySlot(ActorSide.Ally, i);
        }

        enemyStandbySlot = new StandbySlot[size];
        for (uint i = 0; i < enemyStandbySlot.Length; i++)
        {
            enemyStandbySlot[i] = new StandbySlot(ActorSide.Enemy, i);
        }
    }

    /// <summary>
    /// 슬롯에 데이터를 할당하는 함수
    /// </summary>
    /// <param name="data">넣을 데이터</param>
    /// <param name="index">넣을 슬롯의 인덱스</param>
    /// <param name="isStandbySlot">대기석인지 아닌지(true면 대기석, false면 전투석)</param>
    public void AssignSlot(Actor data, uint index, bool isStandbySlot = false)
    {
        if (data is Ally)
        {
            if (!isStandbySlot)
                allySlot[index].AssignData(data);
            else
                allyStandbySlot[index].AssignData(data);
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
    public void InitialAssign(Ally[] allyDatas, OldEnemy[] enemyDatas)
    {
        // 캐릭터 슬롯 채우기
        Queue<Actor> actorQueue = new Queue<Actor>();
        for (int i = 0; i < allyDatas.Length; i++)
        {
            actorQueue.Enqueue(allyDatas[i] as Actor);   // 큐에 캐릭터 데이터들을 다 넣고
        }

        for (int i = 0; i < allySlot.Length; i++)
        {
            Actor temp = actorQueue.Dequeue();                // 큐에서 하나를 꺼내서
            allySlot[i].AssignData(temp);                      // 캐릭터 전투 슬롯을 처음부터 끝까지 채움
        }
        int index = 0;
        while (actorQueue.Count > 0)                               // 큐에 내용물이 남아있는 동안
        {
            Actor temp = actorQueue.Dequeue();                // 큐에서 하나를 꺼내서
            allyStandbySlot[index].AssignData(temp);           // 캐릭터 대기석 맨 앞을 채우고
            index++;                                                // 인덱스를 1 카운트 함
        }
        actorQueue.Clear();

        // 적 슬롯 채우기
        for (int i = 0; i < enemyDatas.Length; i++)
        {
            actorQueue.Enqueue(enemyDatas[i] as Actor);       // 큐에 적 데이터들을 다 넣고
        }

        for (int i = 0; i < enemySlot.Length; i++)
        {
            Actor temp = actorQueue.Dequeue();                // 큐에서 하나를 꺼내서
            enemySlot[i].AssignData(temp);                          // 적 전투 슬롯을 처음부터 끝까지 채움
        }
        index = 0;
        while (actorQueue.Count > 0)                               // 큐에 내용물이 남아있는 동안
        {
            Actor temp = actorQueue.Dequeue();                // 큐에서 하나를 꺼내서
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
        Actor tempData = slotA.ActorData;
        slotA.AssignData(slotB.ActorData);
        slotB.AssignData(tempData);
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
        List<Actor> sortedData = new List<Actor>(Default_Slot_Count);
        foreach (var slot in entityOnField)
        {
            sortedData.Add((slot.ActorData));
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
        SortSlot(allySlot);
        SortSlot(enemySlot);
        SortSlot(allyStandbySlot);
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
            allySlot[i].ClearData();
            enemySlot[i].ClearData();
            allyStandbySlot[i].ClearData();
            enemyStandbySlot[i].ClearData();
        }
    }

    /// <summary>
    /// 전멸한 진영이 있는 지 확인하는 함수
    /// </summary>
    /// <returns>한 진영이라도 전멸했으면 true</returns>
    public bool IsEliminated()
    {
        bool characterEliminated = true;
        foreach (var slot in allySlot)
        {
            if (!slot.IsEmpty)                  // 캐릭터가 하나라도 살아있으면
            {
                characterEliminated = false;    // 전멸 X
            }
        }
        bool enemyEliminated = true;
        foreach (var slot in enemySlot)
        {
            if (!slot.IsEmpty)                  // 적이 하나라도 살아있으면
            {
                enemyEliminated = false;        // 전멸 X
            }
        }
        return characterEliminated || enemyEliminated;  // 두 진영 중 하나라도 전멸 했으면 true
    }

#if UNITY_EDITOR
    public void TestPrint()
    {
        Debug.Log($"[{allySlot[3].ActorData.name}] [{allySlot[2].ActorData.name}] " +
            $"[{allySlot[1].ActorData.name}] [{allySlot[0].ActorData.name}] " +
            $"\n[{enemySlot[0].ActorData.name}] [{enemySlot[1].ActorData.name}] " +
            $"[{enemySlot[2].ActorData.name}] [{enemySlot[3].ActorData.name}]");
    }
#endif
}
