using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SKill_Attack Data", menuName = "Scripable Objects/SKill_Attack Data", order = 4)]
public class Skill_Attack : SkillData, IAttack
{
    public float damageRatio = 1.1f;

    public float criticalRate = 0.25f;

    public float criticalBonus = 0.5f;

    public float DamageRatio => damageRatio;

    public float CriticalRate => criticalRate;

    public float CriticalBonus => criticalBonus;

    public override void ActionExecute(BattleSlot user)
    {
    }
    
    public float FinalDamage(float baseATK)
    {
        float criticalDamage = Random.value < criticalRate ? CriticalBonus : 0;
        float finalDamage = baseATK * Random.Range(DamageRatio * 0.8f, DamageRatio * 1.2f) * (1 + criticalDamage);

        return finalDamage;
    }
}
