using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillGuideLine : MonoBehaviour
{
    SkillData[] skills = null;

    SpriteRenderer[] guideLines;


    Color unvalidColor = new Color(1, 1, 1, 0.3f);

    Vector3 guide0 = new Vector3(-0.95f, -0.35f, 0);
    Vector3 guide1 = new Vector3(0.05f, -0.35f, 0);
    Vector3 guide2 = new Vector3(1.05f, -0.35f, 0);
    Vector3 guide3 = new Vector3(2.05f, -0.35f, 0);

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

    }

    public void OnExecuteEnd()
    {

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

    public void GuideMove(int x)
    {
        
    }

    public void GuideReset()
    {

    }
}
