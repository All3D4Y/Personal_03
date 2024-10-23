using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSkillData : ScriptableObject, IAction
{
    public Sprite icon;

    public uint effectCount = 1;

    public uint effectRange = 1;

    public AffectType affectType;

    public uint EffectCount => effectCount;

    public uint EffectRange => effectRange;

    public AffectType AffectType => affectType;

    protected bool isActive = false;

    public virtual void ActionExecute(BattleSlot user, BattleSlot[] targets)
    {
    }
    
    /// <summary>
    /// 스킬이나 아이템의 효과를 받을 상대를 정하는 함수
    /// </summary>
    /// <param name="user">사용자</param>
    /// <returns>효과를 받을 슬롯들</returns>
    public BattleSlot[] SetTarget(BattleSlot user, AffectType type)
    {
        BattleSlot[] targets = new BattleSlot[EffectCount];

        if (user.Type == EntityType.Charater)   
        {
            if (type == AffectType.Attack || type == AffectType.Debuff)
            {
                for (uint i = 0; i < targets.Length; i++)
                {
                    targets[i] = GameManager.Instance.BattleManager.SlotController.EnemySlot[(EffectRange - (user.Index + 1) + i)];
                }
            }
            else
            {
                for (uint i = 0; i < targets.Length; i++)
                {
                    targets[i] = GameManager.Instance.BattleManager.SlotController.CharacterSlot[i];
                }
            }
        }
        else
        {
            if (type == AffectType.Attack || type == AffectType.Debuff)
            {
                for (uint i = 0; i < targets.Length; i++)
                {
                    targets[i] = GameManager.Instance.BattleManager.SlotController.CharacterSlot[(EffectRange - (user.Index + 1) + i)];
                }
            }
            else
            {
                for (uint i = 0; i < targets.Length; i++)
                {
                    targets[i] = GameManager.Instance.BattleManager.SlotController.EnemySlot[i];
                }
            }
        }
        return targets;
    }
}
