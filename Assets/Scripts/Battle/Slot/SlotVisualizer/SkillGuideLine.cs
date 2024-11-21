using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SkillGuideLine : MonoBehaviour
{
    SkillData[] skills = null;

    SpriteRenderer[] guideLines;


    Color unvalidColor = new Color(1, 1, 1, 0.3f);

    Vector3 guideDefault = new Vector3(-0.95f, -0.35f, 0);


    public void Initialize()
    {
        guideLines = new SpriteRenderer[4];
        for (int i = 0; i < guideLines.Length; i++)
        {
            guideLines[i] = transform.GetChild(i).GetComponent<SpriteRenderer>();
            guideLines[i].color = unvalidColor;
        }
    }
    
    public void OnPrepareEnd()
    {
        GuideOnOff(true);
        SetInitialGuidePosition();
    }

    public void OnExecuteEnd()
    {
        GuideReset();
        GuideOnOff(false);
    }

    public void SetSkill()
    {
        skills = GameManager.Instance.BattleManager.OnTurnSlot.ActorData.skillDatas;
    }

    public void GuideOnOff(bool isOn)
    {
        if (skills != null)
        {
            foreach (var skill in skills)
            {
                if (skill.AffectType == AffectType.Attack)
                {
                    guideLines[(skill.EffectCount - 1)].gameObject.SetActive(isOn);
                }
            } 
        }
    }

    public void GuideAlpha(bool isValid)
    {
        if (skills != null)
        {
            foreach (var skill in skills)
            {
                if (isValid)
                {
                    guideLines[(skill.EffectCount - 1)].color = Color.white;
                }
                else
                {
                    guideLines[(skill.EffectCount - 1)].color = unvalidColor;
                }
            }
        }
    }

    public void GuideReset()
    {
        transform.SetParent(GameManager.Instance.SlotVisualizer.transform);
        for (int i = 0; i < guideLines.Length; i++)
        {
            guideLines[i].transform.position = guideDefault;
            guideLines[i].gameObject.SetActive(false);
        }

        skills = null;
    }

    public void SetInitialGuidePosition()
    {
        float turnX = GameManager.Instance.SlotVisualizer.OnTurn.transform.position.x;
        
        for (int i = 0; i < skills.Length; i++)
        {
            guideLines[i].transform.Translate(new Vector2(skills[i].EffectRange - 2 + turnX, 0));
        }

        transform.SetParent(GameManager.Instance.SlotVisualizer.OnTurn.transform);
    }
}
