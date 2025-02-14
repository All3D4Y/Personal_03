using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "New SKill_Attack Data", menuName = "Scripable Objects/SKill_Attack Data", order = 2)]
public class Skill_Attack : Skill, IAttack
{

    [SerializeField] float ratio = 1.0f;
    [SerializeField] float criticalRate = 0.2f;
    [SerializeField] float criticalBonus = 1.0f;
    [SerializeField] AttackCode code = AttackCode.MeleeAttack;

    public Action<float> onAttackDamage;                // 공격 시 발생한 대미지

    public Action<bool> onCritical;                     // 크리티컬이 발생하면 알리는 델리게이트

    public float Ratio => ratio;                        // 스킬 계수

    public float CriticalRate => criticalRate;          // 크리티컬 확률

    public float CriticalBonus => criticalBonus;        // 크리티컬 대미지 보너스 비율

    public float TempDamage {  get; set; }

    public AttackCode Code => code;

    public override void Affect(Character user, Character target = null)
    {
        if (target != null)
        {
            bool isCritical;
            float dmg = (1 - target.DEF * 0.01f) * DoDamage(user, out isCritical);
            Factory.Instance.GetDamageUI(target.transform.position, dmg, isCritical);
            target.HP -= dmg;
        }
    }

    public float DoDamage(Character user, out bool isCritical)
    {
        float criticalDamage = 0;
        isCritical = false;
        if (Random.value < criticalRate)
        {
            criticalDamage = criticalBonus;
            isCritical = true;
        }
        float finalDamage = user.ATK * ratio * Random.Range( 0.8f, 1.2f) * (1 + criticalDamage);

        if (user.MP <= 0)
            finalDamage *= 0.2f;

        return finalDamage;
    }
}
