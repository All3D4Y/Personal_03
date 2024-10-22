using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "New SKill_Attack Data", menuName = "Scripable Objects/SKill_Attack Data", order = 4)]
public class Skill_Attack : SkillData, IAttack
{
    public float damageRatio = 1.1f;

    public float criticalRate = 0.25f;

    public float criticalBonus = 0.5f;

    public float DamageRatio => damageRatio;

    public float CriticalRate => criticalRate;

    public float CriticalBonus => criticalBonus;

    public override void ActionExecute(BattleSlot user, BattleSlot[] targets)
    {
        float damage = DoDamage(user.EntityData.ATK);
        foreach (var target in targets)
        {
            target.EntityData.HP -= GetDamage(damage, target.EntityData.DEF);
        }
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
