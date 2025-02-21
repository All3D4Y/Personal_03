using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillGroupUI : GroupUIBase
{
    SkillUI[] skillUIs;

    protected override void Awake()
    {
        base.Awake();

        skillUIs = new SkillUI[4];
        for (int i = 0; i < 4; i++)
        {
            skillUIs[i] = transform.GetChild(i).GetComponent<SkillUI>();
        }
    }

    public void Initialize()
    {
        for (int i = 0; i < skillUIs.Length; i++)
        {
            if (!skillUIs[i].IsEmpty)
            {
                skillUIs[i].gameObject.SetActive(true);
                skillUIs[i].Initialize();
            }
            else
                skillUIs[i].gameObject.SetActive(false);
        }
        IsValidTarget();
    }

    public void AssignSkills(Character turnCharacter)
    {
        for (int i = 0; i < turnCharacter.skillDatas.Length; i++)
        {
            skillUIs[i].AssignSkill(turnCharacter.skillDatas[i]);
        }
    }

    public void Clear()
    {
        foreach (SkillUI skillUI in skillUIs)
        {
            skillUI.Clear();
        }
    }

    public void IsValidTarget()
    {
        foreach (SkillUI skillUI in skillUIs)
        {
            skillUI.IsValidTarget();
        }
    }
}
