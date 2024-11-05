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

    SpriteRenderer[] guideRenderers;

    Vector3 guide0 = new Vector3(-0.95f, -0.35f, 0);
    Vector3 guide1 = new Vector3(0.05f, -0.35f, 0);
    Vector3 guide2 = new Vector3(1.05f, -0.35f, 0);
    Vector3 guide3 = new Vector3(2.05f, -0.35f, 0);

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

        guideRenderers = new SpriteRenderer[4];
        child = transform.GetChild(7);
        for (int i = 0; i < guideRenderers.Length; i++)
        {
            guideRenderers[i] = child.GetChild(i).GetComponent<SpriteRenderer>();
        }
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

    public void OnOffGuide(bool isOn)
    {
        if (skill != null && skill.AffectType == AffectType.Attack)
        {
            if (isOn)
            {
                guideRenderers[(skill.EffectCount - 1)].gameObject.SetActive(true); 
            }
            else
            {
                guideRenderers[(skill.EffectCount - 1)].gameObject.SetActive(false);
            }
        }
    }
    public void SetGuideAlpha(bool isValid)
    {
        if (skill != null)
        {
            if (isValid)
            {
                guideRenderers[(skill.EffectCount - 1)].color = new Color(1, 1, 1, 0.3f);
            }
            else
            {
                guideRenderers[(skill.EffectCount - 1)].color = Color.white;
            }
        }
    }
    public void OnMoveGuide(int x)
    {
        if (skill != null)
        {
            Vector3 movePos = guideRenderers[(skill.EffectCount - 1)].transform.localPosition;
            switch (skill.EffectCount - 1)
            {
                case 0:
                    movePos.x = Mathf.Clamp(movePos.x, guide0.x + 1, guide0.x + 2);
                    break;
                case 1:
                    movePos.x = Mathf.Clamp(movePos.x, guide1.x + 1, guide1.x + 1);
                    break;
                case 2:
                    movePos.x = Mathf.Clamp(movePos.x, guide2.x, guide2.x + 1);
                    break;
                case 3:
                    movePos.x = Mathf.Clamp(movePos.x, guide3.x, guide3.x);
                    break;
            }
            movePos += new Vector3(-x, 0, 0);
            guideRenderers[(skill.EffectCount - 1)].transform.localPosition = movePos;
        }
    }

    public void OnReset()
    {
        guideRenderers[0].transform.localPosition = guide0;
        guideRenderers[1].transform.localPosition = guide1;
        guideRenderers[2].transform.localPosition = guide2;
        guideRenderers[3].transform.localPosition = guide3;
    }
}
