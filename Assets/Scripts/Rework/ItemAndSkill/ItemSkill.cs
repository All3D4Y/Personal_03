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

    public abstract void Affect(Character character);
    public void Execute()
    {
        // 실행 로직
    }
    public int[] SetTarget(int index)
    {
        int[] result = new int[count];

        for (int i = 0; i < result.Length; i++)
        {
            result[i] = range - (index + 1) + i;
        }

        return result;
    }
    public int[] SetTarget(Slot user)
    {
        return SetTarget(user.SlotIndex);
    }
    public int[] BuffTarget(int index)
    {
        int[] result = new int[count];
        
        for(int i = 0; i < result.Length; i++)
        {
            result[i] = index - 1 - i;
        }

        return result;
    }
    public int[] BuffTarget(Slot user)
    {
        return BuffTarget(user.SlotIndex);
    }
}
