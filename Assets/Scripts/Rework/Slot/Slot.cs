using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot
{
    public int SlotIndex { get; private set; } // 슬롯의 고유 번호
    public Character CharacterData { get; private set; } // 해당 슬롯에 있는 캐릭터

    public Slot(int index)
    {
        SlotIndex = index;
        CharacterData = null;
    }

    public bool IsEmpty => CharacterData == null;

    public void AssignCharacter(Character character)
    {
        if (!IsEmpty)
            throw new InvalidOperationException("슬롯이 비어있지 않습니다.");
        CharacterData = character;
    }

    public void ClearSlot()
    {
        CharacterData = null;
    }
}