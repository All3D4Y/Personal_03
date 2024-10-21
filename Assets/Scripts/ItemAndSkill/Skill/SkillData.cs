using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillData : ItemSkillData
{
    [Header("스킬 정보")]
    public string skillName = "스킬 이름";
    [TextArea(2, 5)]
    public string skillDescription = "스킬 설명";

    public bool IsActive
    {
        get => isActive;
        set
        {
            isActive = value;
        }
    }
}
