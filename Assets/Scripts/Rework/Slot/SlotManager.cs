using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SlotManager
{
    List<Slot> slots;

    public bool IsPlayer {  get; private set; }

    public SlotManager(int numberOfSlots, bool isPlayer)
    {
        slots = new List<Slot>();
        for (int i = 0; i < numberOfSlots; i++)
        {
            slots.Add(new Slot(i, isPlayer));
        }
        IsPlayer = isPlayer;
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
        if (character != null)
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

    public void MoveCharacter(int fromSlotIndex, int toSlotIndex)
    {
        Slot fromSlot = GetSlot(fromSlotIndex);
        Slot toSlot = GetSlot(toSlotIndex);

        if (!toSlot.IsEmpty)
        {
            Debug.LogWarning("이동하려는 슬롯이 점유 중이거나 차단되었습니다!");
            return;
        }

        Character character = fromSlot.CharacterData;
        if (character == null)
        {
            Debug.LogWarning("이동할 캐릭터가 없습니다!");
            return;
        }

        fromSlot.ClearSlot();              // 원래 슬롯 비우기
        toSlot.AssignCharacter(character); // 목표 슬롯에 캐릭터 할당

        // 캐릭터 오브젝트의 위치 업데이트
        character.transform.position = toSlot.SlotTransform.position;

        Debug.Log($"{character.Name}가 슬롯 {fromSlotIndex}에서 {toSlotIndex}로 이동했습니다.");
    }
}
