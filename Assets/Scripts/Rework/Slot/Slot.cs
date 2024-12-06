using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot
{
    public int SlotIndex { get; private set; }              // 슬롯의 고유 번호
    public Character CharacterData { get; private set; }    // 해당 슬롯에 있는 캐릭터 데이터
    public bool IsPlayer { get; private set; }              // 플레이어 슬롯인지

    public Transform SlotTransform { get; private set; }    // 슬롯의 transform

    public Slot(int index, bool isPlayer)
    {
        SlotIndex = index;
        CharacterData = null;
        SlotTransform = isPlayer ? GameManager.Instance.SlotTransform.PlayerSlot[index] : GameManager.Instance.SlotTransform.EnemySlot[index];
    }

    public bool IsEmpty => CharacterData == null;

    /// <summary>
    /// 슬롯에 캐릭터를 할당하는 함수
    /// </summary>
    /// <param name="character">할당 할 캐릭터</param>
    /// <exception cref="InvalidOperationException"></exception>
    public void AssignCharacter(Character character)
    {
        if (!IsEmpty)
            throw new InvalidOperationException("슬롯이 비어있지 않습니다.");
        CharacterData = character;
        character.Index = SlotIndex;
    }

    /// <summary>
    /// 슬롯을 비우는 함수
    /// </summary>
    public void ClearSlot()
    {
        CharacterData = null;
    }
}