using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SlotManager
{
    Slot[] slots;

    public bool IsPlayer {  get; private set; }
    public int SlotCount { get; private set; }

    /// <summary>
    /// 생성자
    /// </summary>
    /// <param name="numberOfSlots">슬롯의 개 수</param>
    /// <param name="isPlayer">플레이어 슬롯인지</param>
    public SlotManager(int numberOfSlots, bool isPlayer)
    {
        slots = new Slot[numberOfSlots];
        for (int i = 0; i < numberOfSlots; i++)
        {
            slots[i] = new Slot(i, isPlayer);
        }
        IsPlayer = isPlayer;
    }

    /// <summary>
    /// 인덱스로 슬롯을 반환하는 함수
    /// </summary>
    /// <param name="index">인덱스</param>
    /// <returns>인덱스에 해당하는 슬롯</returns>
    public Slot GetSlot(int index)
    {
        Slot result = null;

        if (index < 0 || index >= slots.Length) // 인덱스가 유효하지 않을 때
        {
#if UNITY_EDITOR
            Debug.LogWarning($"슬롯 {index}는 유효하지 않습니다."); 
#endif
        }
        else
            result = slots[index];

        return result;
    }

    /// <summary>
    /// 슬롯에 캐릭터를 할당하는 함수
    /// </summary>
    /// <param name="character">할당할 캐릭터</param>
    /// <param name="index">슬롯의 인덱스</param>
    public void AssignCharacterToSlot(Character character, int index)
    {
        Slot slot = GetSlot(index);
        if (character != null)
            slot.AssignCharacter(character);
    }

    /// <summary>
    /// 슬롯을 비우는 함수
    /// </summary>
    /// <param name="index">슬롯의 인덱스</param>
    public void ClearSlot(int index)
    {
        Slot slot = GetSlot(index);
        slot.ClearSlot();
    }

    /// <summary>
    /// 캐릭터가 할당된 슬롯들을 반환하는 함수
    /// </summary>
    /// <returns>캐릭터가 할당된 슬롯의 리스트</returns>
    public List<Slot> GetOccupiedSlots()
    {
        return slots.Where(slot => !slot.IsEmpty).ToList();
    }

    /// <summary>
    /// 비어있는 슬롯들을 반환하는 함수
    /// </summary>
    /// <returns>비어있는 슬롯의 리스트</returns>
    public List<Slot> GetEmptySlots()
    {
        return slots.Where(slot => slot.IsEmpty).ToList();
    }

    /// <summary>
    /// 슬롯의 캐릭터를 교체하는 함수
    /// </summary>
    /// <param name="fromSlotIndex">교체할 슬롯 1</param>
    /// <param name="toSlotIndex">교체할 슬롯 2</param>
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

                
                OnMoveTransform(fromCharacter, toSlot.SlotTransform.position);      // 원래 슬롯의 캐릭터를 목표 슬롯의 위치로
                OnMoveTransform(toCharacter, fromSlot.SlotTransform.position);      // 목표 슬롯의 캐릭터를 원래 슬롯의 위치로
            }                                                                 
            fromSlot.CharacterData.CUI.TransformUpdate();
            toSlot.CharacterData.CUI.TransformUpdate();
#if UNITY_EDITOR
            Debug.Log($"{fromSlot.CharacterData.Name}가 슬롯 {fromSlotIndex}에서 {toSlotIndex}로 이동했습니다."); 
#endif
        }
        else
        {
#if UNITY_EDITOR
            Debug.LogWarning($"{fromSlotIndex}번 째 슬롯에는 이동할 캐릭터가 없습니다!"); 
#endif
        }
    }

    /// <summary>
    /// 슬롯을 한 칸 이동시키는 함수
    /// </summary>
    /// <param name="input">+1 -> 인덱스++, -1 -> 인덱스--</param>
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

    /// <summary>
    /// 빈 슬롯이 생겼을 때 슬롯을 재정렬하는 함수
    /// </summary>
    public void ReorderSlots()
    {
        // 살아있는 캐릭터만 있는 리스트를 만들고 슬롯 비우기
        List<Character> list = new List<Character>();
        foreach (Slot slot in slots)
        {
            if (!slot.IsEmpty)
            {
                if (slot.CharacterData.IsAlive)
                {
                    list.Add(slot.CharacterData);
                }
                slot.ClearSlot();
            }
        }
        // 살아있는 캐릭터들만 재배치하기
        int i = 0;
        foreach (Character character in list)
        {
            AssignCharacterToSlot(character, i);
            if (IsPlayer)
                OnMoveTransform(character, GameManager.Instance.SlotTransform.PlayerSlot[i].position);
            else
                OnMoveTransform(character, GameManager.Instance.SlotTransform.EnemySlot[i].position);
            character.CUI.TransformUpdate();
            i++;
        }
    }

    /// <summary>
    /// 캐릭터를 부드럽게 이동시키는 함수
    /// </summary>
    /// <param name="character">이동시킬 캐릭터</param>
    /// <param name="toPos">이동시킬 위치</param>
    void OnMoveTransform(Character character, Vector3 toPos)
    {
        GameManager.Instance.CoroutineManager.OnMoveCharacter(character, toPos);
    }
}
