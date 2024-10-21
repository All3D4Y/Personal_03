using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDataManager : MonoBehaviour
{
    public TestSkill[] testSkills;

    public TestSkill this[uint index]
    {
        get => testSkills[index];
        set => testSkills[index] = value;
    }
}
