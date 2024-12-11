using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemSkill : ScriptableObject
{
    public Sprite icon;
    public string iS_Name;
    [TextArea(2, 5)]
    public string iS_Description;
    [SerializeField] protected int count = 1;
    [SerializeField] protected int range = 1;
    [SerializeField] protected int mpCost;

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
            targets = SetTargetIndex(turn.Index);
            foreach (int target in targets)
            {
                if (turn.IsPlayer)
                    Affect(turn, battleManager.EnemySlot.GetSlot(target).CharacterData);
                else
                    Affect(turn, battleManager.PlayerSlot.GetSlot(target).CharacterData);
            }
        }
        else if (this is IBuff)
        {
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
    public int[] SetTargetIndex(int index)
    {
        int[] result = new int[count];

        for (int i = 0; i < result.Length; i++)
        {
            result[i] = range - (index + 1) + i;
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
            result[i] = index - 1 - i;
        }

        return result;
    }
    public int[] BuffTargetIndex(Slot user)
    {
        return BuffTargetIndex(user.SlotIndex);
    }
}
