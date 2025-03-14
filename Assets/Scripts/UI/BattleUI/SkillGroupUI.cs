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

    /// <summary>
    /// 초기화
    /// </summary>
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

    /// <summary>
    /// UI들에 스킬을 일괄 할당하는 함수
    /// </summary>
    /// <param name="turnCharacter"></param>
    public void AssignSkills(Character turnCharacter)
    {
        for (int i = 0; i < turnCharacter.skillDatas.Length; i++)
        {
            skillUIs[i].AssignSkill(turnCharacter.skillDatas[i]);
        }
    }

    /// <summary>
    /// UI에 할당된 스킬을 일괄 삭제하는 함수
    /// </summary>
    public void Clear()
    {
        foreach (SkillUI skillUI in skillUIs)
        {
            skillUI.Clear();
        }
    }

    /// <summary>
    /// UI의 스킬이 유효한지 일괄 확인, 색상 변경 및 버튼 기능 활성화를 처리하는 함수
    /// </summary>
    public void IsValidTarget()
    {
        foreach (SkillUI skillUI in skillUIs)
        {
            skillUI.IsValidTarget();
        }
    }
}
