using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    Skill skill = null;
    GuideLine guideLine = null;

    Image board;

    Button button;
    Image skillIcon;
    TextMeshProUGUI count;
    TextMeshProUGUI range;
    TextMeshProUGUI skillName;
    TextMeshProUGUI skillDescription;
    TextMeshProUGUI mpCost;

    Color invalidColor = new Color(1, 1, 1, 0.2f);  // 스킬 사용이 유효하지 않을 때 UI의 색상

    public bool IsEmpty => skill == null;           // 이 UI에 스킬 정보가 할당되어 있는지를 확인하는 프로퍼티

    
    public GuideLine GuideLine => guideLine;        // 스킬 거리 확인용 보조선

    void Awake()
    {
        guideLine = transform.GetChild(0).GetComponent<GuideLine>();

        Transform child = transform.GetChild(2);

        button = child.GetComponent<Button>();
        button.onClick.AddListener(UIExecution);

        board = child.GetComponent<Image>();

        child = child.GetChild(0).GetChild(0);
        skillIcon = child.GetComponent<Image>();
        child = transform.GetChild(2).GetChild(1);
        count = child.GetComponent<TextMeshProUGUI>();
        child = transform.GetChild(2).GetChild(2);
        range = child.GetComponent<TextMeshProUGUI>();
        child = transform.GetChild(2).GetChild(3);
        skillName = child.GetComponent<TextMeshProUGUI>();
        child = transform.GetChild(2).GetChild(4);
        skillDescription = child.GetComponent<TextMeshProUGUI>();
        child = transform.GetChild(2).GetChild(6);
        mpCost = child.GetComponent<TextMeshProUGUI>();
    }

    /// <summary>
    /// 초기화
    /// </summary>
    public void Initialize()
    {
        if (!IsEmpty)
        {
            // 스킬 정보 UI에 적용
            skillIcon.sprite = skill.icon;

            string temp = skill.Count == 1 ? "[Single]" : "[Multi]";
            count.text = temp;
            range.text = $"[Range : {skill.Range}]";
            skillName.text = skill.iS_Name;
            skillDescription.text = skill.iS_Description;
            mpCost.text = skill.MPCost.ToString();

            // 가이드라인 초기화
            guideLine.ResetGuide();

            if (skill.Range != 0)
            {
                if (skill is IBuff)
                {
                    IBuff buff = skill as IBuff;
                    if (buff.IsDebuff)
                    {
                        guideLine.Initialize(skill.Count, transform.GetSiblingIndex());
                        guideLine.TransformUpdate(skill.Range, skill.Count);
                    }
                }
                else
                {
                    guideLine.Initialize(skill.Count, transform.GetSiblingIndex());
                    guideLine.TransformUpdate(skill.Range, skill.Count);
                }
            }

            // 타겟이 유효한지 검사
            IsValidTarget();
        }
        else
        {
#if UNITY_EDITOR
            Debug.LogWarning($"이 UI ({this.name})에는 할당된 스킬이 없어 초기화를 실행하지 못 했습니다!"); 
#endif
        }
    }

    /// <summary>
    /// UI에 스킬 정보를 할당하는 함수
    /// </summary>
    /// <param name="skill"></param>
    public void AssignSkill(Skill skill)
    {
        if (IsEmpty)
        {
            this.skill = skill;
        }
        else
        {
#if UNITY_EDITOR
            Debug.LogWarning($"UI ({this.name})에는 이미 할당된 스킬 정보가 있습니다!"); 
#endif
        }
    }

    /// <summary>
    /// UI에 할당된 스킬 정보를 삭제하는 함수
    /// </summary>
    public void Clear()
    {
        skill = null;
    }

    /// <summary>
    /// 버튼 클릭 시 할당된 스킬을 ActionManager에 저장, 행동 실행 상태로 넘어가면 실행
    /// </summary>
    public void UIExecution()
    {
        GameManager.Instance.BattleManager.ActionManager.SetAction(skill);
        GameManager.Instance.BattleManager.ChangeState<Execution>();
    }

#if UNITY_EDITOR
    public void TestDebug()
    {
        Debug.Log("Skill Clicked");
    } 
#endif

    /// <summary>
    /// 스킬의 효과 적용이 유효한지 확인하고 유효하지 않으면 버튼 기능을 비활성화, UI의 색상을 변경하는 함수
    /// </summary>
    public void IsValidTarget()
    {
        if (!IsEmpty)
        {
            if (skill.IsValid())
            {
                board.color = Color.white;
                button.interactable = true;
                guideLine.ValidColor(true);
            }
            else
            {
                board.color = invalidColor;
                button.interactable = false;
                guideLine.ValidColor(false);
            }
            guideLine.TransformUpdate(skill.Range, skill.Count);
        }
    }
}
