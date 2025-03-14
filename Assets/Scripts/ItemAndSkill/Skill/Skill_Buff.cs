using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SKill_Buff Data", menuName = "Scripable Objects/SKill_Buff Data", order = 3)]
public class Skill_Buff : Skill, IBuff
{
    [SerializeField] BuffType type = BuffType.Attack;
    [SerializeField] int duration = 0;
    [SerializeField] float ratio = 0.3f;
    [SerializeField] bool isDebuff = false;

    public BuffType Type => type;
    public int Duration => duration;
    public int ElapsedTurn { get; set; }
    public float Ratio => ratio;
    public bool IsDebuff => isDebuff;

    public override void Affect(Character user, Character target)
    {
        ratio *= isDebuff ? 1 : -1;
        switch ((int)type)
        {
            case 0:
                target.ATK *= (1 + ratio);
                break;
            case 1:
                target.DEF *= (1 + ratio);
                break;
            case 2:
                target.CurrentSpeedIncrement *= (1 + ratio);
                break;
        }
        Debug.Log($"{target.Name}이 {duration}턴 동안 {type}의 효과를 받습니다...");
    }
}
