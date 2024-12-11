using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    ItemSkill skill = null;

    Button button;
    Image skillIcon;
    TextMeshProUGUI count;
    TextMeshProUGUI range;
    TextMeshProUGUI skillName;
    TextMeshProUGUI skillDescription;
    TextMeshProUGUI mpCost;

    public bool IsEmpty => skill == null;

    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(UIExecution);

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

    public void Initialize()
    {
        if (!IsEmpty)
        {
            skillIcon.sprite = skill.icon;

            string temp = skill.Count == 1 ? "[Single]" : "[Multi]";
            count.text = temp;
            range.text = $"[Range : {skill.Range}]";
            skillName.text = skill.iS_Name;
            skillDescription.text = skill.iS_Description;
            mpCost.text = skill.MPCost.ToString();
        }
        else
        {
            Debug.LogWarning($"이 UI ({this.name})에는 할당된 스킬이 없어 초기화를 실행하지 못 했습니다!");
        }
    }

    public void AssignSkill(ItemSkill skill)
    {
        if (IsEmpty)
        {
            this.skill = skill;
        }
        else
        {
            Debug.LogWarning($"UI ({this.name})에는 이미 할당된 스킬 정보가 있습니다!");
        }
    }

    public void Clear()
    {
        skill = null;
    }

    public void UIExecution()
    {
        GameManager.Instance.BattleManager.ActionManager.SetAction(skill);
        GameManager.Instance.BattleManager.ChangeState<Execution>();
    }

    public void TestDebug()
    {
        Debug.Log("Skill Clicked");
    }
}
