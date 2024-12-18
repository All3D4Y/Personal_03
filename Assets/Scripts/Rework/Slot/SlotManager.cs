using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SlotManager
{
    List<Slot> slots;

    public bool IsPlayer {  get; private set; }
    public int SlotCount { get; private set; }

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
        return slots.Where(slot => !slot.IsEmpty).ToList();
    }

    public List<Slot> GetEmptySlots()
    {
        return slots.Where(slot => slot.IsEmpty).ToList();
    }

    public void SwapCharacter(int fromSlotIndex, int toSlotIndex)
    {
        Slot fromSlot = GetSlot(fromSlotIndex);
        Slot toSlot = GetSlot(toSlotIndex);

        if (!toSlot.IsEmpty)                                                        // 원래 슬롯에 캐릭터가 있다면
        {
            Character fromCharacter = fromSlot.CharacterData;
            if (toSlot.IsEmpty)                                                     // 목표 슬롯이 비었으면
            {
                fromSlot.ClearSlot();                                               // 원래 슬롯 비우기
                toSlot.AssignCharacter(fromCharacter);                              // 목표 슬롯에 캐릭터 할당

                fromCharacter.transform.position = toSlot.SlotTransform.position;   // 위치 업데이트
            }
            else                                                                    // 목표 슬롯에 캐릭터가 있으면
            {
                Character toCharacter = toSlot.CharacterData;
                fromSlot.ClearSlot();                                               // 원래 슬롯 비우기
                toSlot.ClearSlot();                                                 // 목표 슬롯 비우기
                toSlot.AssignCharacter(fromCharacter);                              // 목표 슬롯에 캐릭터 할당
                fromSlot.AssignCharacter(toCharacter);                              // 원래 슬롯에 캐릭터 할당

                fromCharacter.transform.position = toSlot.SlotTransform.position;   // 원래 슬롯의 캐릭터를 목표 슬롯의 위치로
                toCharacter.transform.position = fromSlot.SlotTransform.position;   // 목표 슬롯의 캐릭터를 원래 슬롯의 위치로
            }
            Debug.Log($"{fromSlot.CharacterData.Name}가 슬롯 {fromSlotIndex}에서 {toSlotIndex}로 이동했습니다.");
        }
        else
            Debug.LogWarning($"{fromSlotIndex}번 째 슬롯에는 이동할 캐릭터가 없습니다!");
    }

    public void OnMoveSlot(int input)
    {
        BattleManager battleManager = GameManager.Instance.BattleManager;
        Character onTurn = battleManager.OnTurnCharacter;

        if (-1 < onTurn.Index && onTurn.Index < battleManager.PlayerParty.Count)
        {
            if (onTurn.Index + input < 0 || onTurn.Index + input >= battleManager.PlayerParty.Count)
            {
                Debug.LogWarning($"{onTurn.Name}은 이 방향으로 더 움직일 수 없습니다!");
            }
            else
            {
                SwapCharacter(onTurn.Index, onTurn.Index + input);
            }
        }
    }
}
