using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "New SKill_Attack Data", menuName = "Scripable Objects/SKill_Attack Data", order = 2)]
public class OldSkill_Attack : SkillData, IOldAttack
{
    public float damageRatio = 1.1f;

    public float criticalRate = 0.25f;

    public float criticalBonus = 0.5f;

    public float DamageRatio => damageRatio;

    public float CriticalRate => criticalRate;

    public float CriticalBonus => criticalBonus;


    public override void ActionExecute(BattleSlot user, BattleSlot[] targets)
    {
        float damage = DoDamage(user.ActorData.ATK);
        foreach (var target in targets)
        {
            if (target != null)
            {
                target.Passer = new DamagePasser(GetDamage(damage, target.ActorData.DEF));
            }
        }
        user.ActorData.MP -= MPCost;
    }
    
    public float DoDamage(float baseATK)
    {
        float criticalDamage = Random.value < criticalRate ? CriticalBonus : 0;
        float finalDamage = baseATK * Random.Range(DamageRatio * 0.8f, DamageRatio * 1.2f) * (1 + criticalDamage);

        return finalDamage;
    }

    public float GetDamage(float damage, float def)
    {
        return damage - (def * damage * 0.01f);
    }
}
