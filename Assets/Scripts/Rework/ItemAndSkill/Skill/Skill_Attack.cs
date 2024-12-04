using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Attack : ItemSkill, IAttack
{
    [SerializeField] float ratio = 1.0f;
    [SerializeField] float criticalRate = 0.2f;
    [SerializeField] float criticalBonus = 1.0f;

    public float Ratio => ratio;

    public float CriticalRate => criticalRate;

    public float CriticalBonus => criticalBonus;

    public override void Affect(Character character)
    {
    }
    public override void SetTarget()
    {
    }

    public float DoDamage(float atk)
    {
        float result = 0;
        return result;
    }

}
