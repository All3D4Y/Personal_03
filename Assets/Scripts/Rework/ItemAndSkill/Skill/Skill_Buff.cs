using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Buff : ItemSkill, IBuff
{
    [SerializeField] BuffType type = BuffType.Attack;
    [SerializeField] int duration = 0;
    [SerializeField] float ratio = 0.3f;
    [SerializeField] bool isDebuff = false;

    public BuffType Type => type;
    public int Duration => duration;
    public float Ratio => ratio;
    public bool IsDebuff => isDebuff;

    public override void Affect(Character character)
    {
        ratio *= isDebuff ? 1 : -1;
        switch ((int)type)
        {
            case 0:
                character.ATK *= (1 + ratio);
                break;
            case 1:
                character.DEF *= (1 + ratio);
                break;
            case 2:
                character.CurrentSpeedIncrement *= (1 + ratio);
                break;
        }
    }
}
