using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Skill_Attack : ItemSkill, IAttack
{
    [SerializeField] float ratio = 1.0f;
    [SerializeField] float criticalRate = 0.2f;
    [SerializeField] float criticalBonus = 1.0f;

    public Action<bool> onCritical;                     // 크리티컬이 발생하면 알리는 델리게이트

    public float Ratio => ratio;                        // 스킬 계수

    public float CriticalRate => criticalRate;          // 크리티컬 확률

    public float CriticalBonus => criticalBonus;        // 크리티컬 대미지 보너스 비율

    public override void Affect(Character character)
    {
    }

    public float DoDamage(float atk)
    {
        float criticalDamage = 0;
        if (Random.value < criticalRate)
        {
            criticalDamage = criticalBonus;
            onCritical?.Invoke(true);
        }
        float finalDamage = atk * ratio * Random.Range( 0.8f, 1.2f) * (1 + criticalDamage);

        return finalDamage;
    }
}
