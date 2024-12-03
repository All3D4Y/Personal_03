using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SlotManager
{
    private List<Slot> slots;

    public SlotManager(int numberOfSlots)
    {
        slots = new List<Slot>();
        for (int i = 0; i < numberOfSlots; i++)
        {
            slots.Add(new Slot(i));
        }
    }

    public Slot GetSlot(int index)
    {
        if (index < 0 || index >= slots.Count)
            throw new ArgumentOutOfRangeException($"슬롯 {index}는 유효하지 않습니다.");
        return slots[index];
    }

    public void AssignCharacterToSlot(Character character, int index)
    {
        Slot slot = GetSlot(index);
        slot.AssignCharacter(character);
    }

    public void ClearSlot(int index)
    {
        Slot slot = GetSlot(index);
        slot.ClearSlot();
    }

    public List<Slot> GetOccupiedSlots()
    {
        return slots.Where(slot => slot.IsEmpty).ToList();
    }

    public List<Slot> GetEmptySlots()
    {
        return slots.Where(slot => !slot.IsEmpty).ToList();
    }

    // 나중에 상태머신으로 이동
    void InitializeBattle()
    {
        // 슬롯 초기화
        SlotManager playerSlots = new SlotManager(4); // 아군 슬롯 4개
        SlotManager enemySlots = new SlotManager(4);  // 적 슬롯 4개
        SlotManager playerStandbySlots = new SlotManager(4); // 
        SlotManager enemyStandbySlots = new SlotManager(4);

        //PlayerDataManager.Instance.players

        // 아군 배치

        //playerSlots.AssignCharacterToSlot();

        // 적 배치
        //enemySlots.AssignCharacterToSlot();
        //enemySlots.AssignCharacterToSlot();
    }
}
