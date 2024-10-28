using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    SkillData skill = null;

    Image skillIcon;
    TextMeshProUGUI count;
    TextMeshProUGUI range;
    TextMeshProUGUI skillName;
    TextMeshProUGUI skillDescription;
    TextMeshProUGUI mpCost;

    LineRenderer lineRenderer;

    public bool IsEmpty => skill == null;

    public void Initialize()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;

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

    public void SetActor(Actor actor, int index)
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

    public void DrawRangeGuide()
    {
        if (skill != null)
        {
            switch (skill.EffectCount)
            {
                case 1:
                    lineRenderer.positionCount = 3;

                    break;
                case 2:
                    lineRenderer.positionCount = 8;
                    break;
                case 3:
                    lineRenderer.positionCount = 10;
                    break;
                case 4:
                    lineRenderer.positionCount = 14;
                    break;
            }
        }
    }

    void DrawLine(int startIndex, int endIndex, Vector3 startVec, Vector3 endVec)
    {
        if (lineRenderer.enabled == false)
        {
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(startIndex, startVec);
            lineRenderer.SetPosition(endIndex, endVec);
        }
    }
}
