using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    SkillData skill = null;

    Button button;
    Image skillIcon;
    TextMeshProUGUI count;
    TextMeshProUGUI range;
    TextMeshProUGUI skillName;
    TextMeshProUGUI skillDescription;
    TextMeshProUGUI mpCost;

    public bool IsEmpty => skill == null;

    public void Initialize()
    {
        button = GetComponent<Button>();

        Transform child;
        child = transform.GetChild(0).GetChild(0);
        skillIcon = child.GetComponent<Image>();
        child = transform.GetChild(1);
        count = child.GetComponent<TextMeshProUGUI>();
        child = transform.GetChild(2);
        range = child.GetComponent<TextMeshProUGUI>();
        child = transform.GetChild(3);
        skillName = child.GetComponent<TextMeshProUGUI>();
        child = transform.GetChild(4);
        skillDescription = child.GetComponent<TextMeshProUGUI>();
        child = transform.GetChild(6);
        mpCost = child.GetComponent<TextMeshProUGUI>();
    }

    /// <summary>
    /// 스킬 UI에 스킬 정보를 넣는 함수
    /// </summary>
    /// <param name="actor">스킬을 가진 Actor</param>
    /// <param name="index">스킬의 인덱스</param>
    public void SetSkill(Actor actor, int index)
    {
        if (actor != null)
        {
            skill = actor.skillDatas[index];
        
            skillIcon.sprite = skill.Icon;

            string temp = skill.EffectCount == 1 ? "[Single]" : "[Multi]";
            count.text = temp;
            range.text = $"[Range : {skill.EffectRange}]";
            skillName.text = skill.skillName;
            skillDescription.text = skill.skillDescription;
            mpCost.text = skill.MPCost.ToString();
        }
    }

    public void Clear()
    {
        skill = null;
    }
}
