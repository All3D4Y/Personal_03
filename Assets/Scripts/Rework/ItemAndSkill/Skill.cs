﻿using System.Collections;
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

    public int Count => count;
    public int Range => range;
    public int MPCost => mpCost;

    public abstract void Affect(Character user, Character target);
    public void Execute()
    {
        // 실행 로직
        BattleManager battleManager = GameManager.Instance.BattleManager;
        Character turn = battleManager.OnTurnCharacter;
        int[] targets = new int[count];
        if (this is IAttack)
        {
            if (range > 0)
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
            else
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
        else if (this is IBuff)
        {
            Skill_Buff buff = this as Skill_Buff;
            if (!buff.IsDebuff)
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
            else
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
        else if (this is IHeal)
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
    public int[] SetTargetIndex(int userIndex)
    {
        int[] result = new int[count];

        for (int i = 0; i < result.Length; i++)
        {
            result[i] = range - (userIndex + 1) + i;
        }

        return result;
    }
    public int[] SetTargetIndex(Slot user)
    {
        return SetTargetIndex(user.SlotIndex);
    }
    public int[] BuffTargetIndex(int index)
    {
        int[] result = new int[count];
        
        for(int i = 0; i < result.Length; i++)
        {
            result[i] = index - range - i;
        }

        return result;
    }
    public int[] BuffTargetIndex(Slot user)
    {
        return BuffTargetIndex(user.SlotIndex);
    }
    public bool IsValid(int userIndex)
    {
        bool result = false;
        int[] temp;

        BattleManager battleManager = GameManager.Instance.BattleManager;

        if (this is Skill_Attack)
        {
            temp = SetTargetIndex(userIndex);
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

        return result;
    }

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
