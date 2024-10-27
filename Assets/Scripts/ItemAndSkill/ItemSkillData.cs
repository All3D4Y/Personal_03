using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSkillData : ScriptableObject, IAction
{
    [SerializeField] protected Sprite icon;

    [SerializeField] protected uint effectCount = 1;

    [SerializeField] protected uint effectRange = 1;

    [SerializeField] protected AffectType affectType;

    [SerializeField] protected float mpCost;

    public uint EffectCount => effectCount;

    public uint EffectRange => effectRange;

    public AffectType AffectType => affectType;

    public float MPCost => mpCost;

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

        if (user.Side == ActorSide.Ally)   
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
                    targets[i] = GameManager.Instance.BattleManager.SlotController.AllySlot[i];
                }
            }
        }
        else
        {
            if (type == AffectType.Attack || type == AffectType.Debuff)
            {
                for (uint i = 0; i < targets.Length; i++)
                {
                    targets[i] = GameManager.Instance.BattleManager.SlotController.AllySlot[(EffectRange - (user.Index + 1) + i)];
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
