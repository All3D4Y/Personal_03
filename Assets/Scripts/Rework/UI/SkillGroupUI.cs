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
        foreach (SkillUI skillUI in skillUIs)
        {
            skillUI.Initialize();
            if (!skillUI.IsEmpty)
                skillUI.gameObject.SetActive(true);
            else
                skillUI.gameObject.SetActive(false);
        }
    }

    public void AssignSkills(Character turnCharacter)
    {
        for (int i = 0; i < turnCharacter.skillDatas.Length; i++)
        {
            skillUIs[i].AssignSkill(turnCharacter.skillDatas[i]);
        }
    }


}
