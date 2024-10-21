using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSkillData : ScriptableObject, IAction
{
    public Sprite icon;

    public uint effectCount = 1;

    public uint effectRange = 1;

    public uint EffectCount => effectCount;

    public uint EffectRange => effectRange;

    protected bool isActive = false;

    public virtual void ActionExecute(BattleSlot user)
    {
    }
    
    public (uint, uint) SetTarget(BattleSlot user)
    {
        BattleSlot[] targets = new BattleSlot[EffectCount];

        uint tagetIndex_0 = EffectRange - (user.Index + 1);

        return (tagetIndex_0, EffectCount);
    }
}
