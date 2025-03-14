using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : ScriptableObject
{
    public Sprite icon;
    public string iS_Name;
    [TextArea(2, 5)]
    public string iS_Description;
    [SerializeField] protected int count = 1;
    [SerializeField] protected int range = 1;
    [SerializeField] protected int mpCost = 0;

    /// <summary>
    /// 효과를 적용할 대상의 수
    /// </summary>
    public int Count => count;

    /// <summary>
    /// 효과를 줄 대상까지의 거리
    /// </summary>
    public int Range => range;

    /// <summary>
    /// 사용하기 위해 필요한 마인트 포인트
    /// </summary>
    public int MPCost => mpCost;

    /// <summary>
    /// 효과의 내용
    /// </summary>
    /// <param name="user">사용자</param>
    /// <param name="target">적용 대상</param>
    public abstract void Affect(Character user, Character target);

    /// <summary>
    /// 실행 함수
    /// </summary>
    public void Execute()
    {
        // 실행 로직
        BattleManager battleManager = GameManager.Instance.BattleManager;
        Character turn = battleManager.OnTurnCharacter;
        int[] targets = new int[count];
        if (this is IAttack)        // 공격이라면
        {
            if (range > 0)          // 사거리 확인
            {
                targets = SetTargetIndex(turn.Index);
                foreach (int target in targets)
                {
                    if (target >= 0)
                    {
                        if (turn.IsPlayer)
                            Affect(turn, battleManager.EnemySlot.GetSlot(target).CharacterData);
                        else
                            Affect(turn, battleManager.PlayerSlot.GetSlot(target).CharacterData);
                    }
                } 
            }
            else                    // 달걀 공격
            {
                if (count == 4)
                {
                    targets = new int[] { 0, 1, 2, 3 };
                    foreach (int target in targets)
                    {
                        Affect(turn, battleManager.EnemySlot.GetSlot(target).CharacterData);
                    }
                }
            }
        }
        else if (this is IBuff)     // 버프라면
        {
            Skill_Buff buff = this as Skill_Buff;
            if (!buff.IsDebuff)     // 디버프가 아니면
            {
                targets = BuffTargetIndex(turn.Index);
                foreach (int target in targets)
                {
                    if (turn.IsPlayer)
                        battleManager.PlayerSlot.GetSlot(target).CharacterData.BuffManager.AddBuff(buff);
                    else
                        battleManager.EnemySlot.GetSlot(target).CharacterData.BuffManager.AddBuff(buff);
                } 
            }
            else                    // 디버프면
            {
                targets = SetTargetIndex(turn.Index);
                foreach (int target in targets)
                {
                    if (turn.IsPlayer)
                        battleManager.EnemySlot.GetSlot(target).CharacterData.BuffManager.AddBuff(buff);
                    else
                        battleManager.PlayerSlot.GetSlot(target).CharacterData.BuffManager.AddBuff(buff);
                }
            }
        }
        else if (this is IHeal)     // 힐이면
        {
            Item_Heal heal = this as Item_Heal;
            targets = BuffTargetIndex(turn.Index);
            foreach (int target in targets)
            {
                if (turn.IsPlayer)
                    Affect(turn, battleManager.PlayerSlot.GetSlot(target).CharacterData);
                else
                    Affect(turn, battleManager.EnemySlot.GetSlot(target).CharacterData);
            }
        }
    }

    /// <summary>
    /// 적용 대상의 인덱스의 배열을 반환하는 함수
    /// </summary>
    /// <param name="userIndex">사용자의 인덱스</param>
    /// <returns>적용 대상의 인덱스들</returns>
    public int[] SetTargetIndex(int userIndex)
    {
        int[] result = new int[count];

        for (int i = 0; i < result.Length; i++)
        {
            result[i] = range - (userIndex + 1) + i;
        }

        return result;
    }

    /// <summary>
    /// 적용 대상의 인덱스의 배열을 반환하는 함수
    /// </summary>
    /// <param name="user">사용자의 슬롯</param>
    /// <returns>적용 대상의 인덱스들</returns>
    public int[] SetTargetIndex(Slot user)
    {
        return SetTargetIndex(user.SlotIndex);
    }

    /// <summary>
    /// 버프 대상의 인덱스의 배열을 반환하는 함수
    /// </summary>
    /// <param name="userIndex">사용자의 인덱스</param>
    /// <returns>버프 대상의 인덱스들</returns>
    public int[] BuffTargetIndex(int userIndex)
    {
        int[] result = new int[count];
        
        for(int i = 0; i < result.Length; i++)
        {
            result[i] = userIndex - range - i;
        }

        return result;
    }

    /// <summary>
    /// 버프 대상의 인덱스의 배열을 반환하는 함수
    /// </summary>
    /// <param name="user">사용자의 슬롯</param>
    /// <returns>버프 대상의 인덱스들</returns>
    public int[] BuffTargetIndex(Slot user)
    {
        return BuffTargetIndex(user.SlotIndex);
    }

    /// <summary>
    /// 효과의 적용 대상이 적정거리에 존재하는지(효과가 유효한지) 확인하는 함수
    /// </summary>
    /// <param name="userIndex">사용자의 인덱스</param>
    /// <returns>유효하면 true, 아니면 false</returns>
    public bool IsValid(int userIndex)
    {
        bool result = false;
        int[] temp;

        BattleManager battleManager = GameManager.Instance.BattleManager;

        if (this is Skill_Attack)       // 공격 스킬이라면
        {
            temp = SetTargetIndex(userIndex);
            foreach (int i in temp)
            {
                if (i >= 0 && !battleManager.EnemySlot.GetSlot(i).IsEmpty)
                    result |= true;
            }
        }
        else if (this is Skill_Buff)    // 버프라면
        {
            Skill_Buff buff = this as Skill_Buff;
            if (!buff.IsDebuff)
            {
                temp = BuffTargetIndex(userIndex);
                foreach (int i in temp)
                {
                    if (i >= 0 && !battleManager.PlayerSlot.GetSlot(i).IsEmpty)
                        result |= true;
                } 
            }
            else
            {
                temp = SetTargetIndex(userIndex);
                foreach (int i in temp)
                {
                    if (i >= 0 && !battleManager.EnemySlot.GetSlot(i).IsEmpty)
                        result |= true;
                }
            }
        }
        else if (this is Item_Attack)   // 공격 아이템이면
        {
            if (range == 0)
            {
                for (int i = 0; i < 4; i++)
                {
                    result |= !battleManager.EnemySlot.GetSlot(i).IsEmpty;
                }
            }
        }

        return result;
    }


    /// <summary>
    /// 효과의 적용 대상이 적정거리에 존재하는지(효과가 유효한지) 확인하는 함수
    /// </summary>
    /// <returns>유효하면 true, 아니면 false</returns>
    public bool IsValid()
    {
        bool result = false;
        int[] temp;

        BattleManager battleManager = GameManager.Instance.BattleManager;

        if (this is Skill_Attack)
        {
            temp = SetTargetIndex(battleManager.OnTurnCharacter.Index);
            foreach (int i in temp)
            {
                if (i >= 0 && !battleManager.EnemySlot.GetSlot(i).IsEmpty)
                    result |= true;
            }
        }
        else if (this is Skill_Buff)
        {
            Skill_Buff buff = this as Skill_Buff;
            if (!buff.IsDebuff)
            {
                temp = BuffTargetIndex(battleManager.OnTurnCharacter.Index);
                foreach (int i in temp)
                {
                    if (i >= 0 && !battleManager.PlayerSlot.GetSlot(i).IsEmpty)
                        result |= true;
                }
            }
            else
            {
                temp = SetTargetIndex(battleManager.OnTurnCharacter.Index);
                foreach (int i in temp)
                {
                    if (i >= 0 && !battleManager.EnemySlot.GetSlot(i).IsEmpty)
                        result |= true;
                }
            }
        }
        else if (this is Item_Attack)
        {
            if (range == 0)
            {
                for (int i = 0; i < 4; i++)
                {
                    result |= !battleManager.EnemySlot.GetSlot(i).IsEmpty;
                }
            }
        }
        else if (this is Item_Heal)
        {
            temp = BuffTargetIndex(battleManager.OnTurnCharacter.Index);
            foreach(int i in temp)
            {
                if (i >= 0 && !battleManager.PlayerSlot.GetSlot(i).IsEmpty)
                    result |= true;
            }
        }

        return result;
    }
}
