using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SkillGuideLine : MonoBehaviour
{
    SkillData[] skills = null;

    SpriteRenderer[] guideLines;


    Color unvalidColor = new Color(1, 1, 1, 0.15f);

    Vector3 guideDefaultPos = new Vector3(-0.95f, -0.35f, 0);


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
        OnActivateGuide();
    }

    public void OnExecuteEnd()
    {
        OnDeactivateGuide();
    }

    public void SetSkill()
    {
        skills = OldGameManager.Instance.BattleManager.OnTurnSlot.ActorData.skillDatas;
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

    public void GuideAlpha()
    {
        BattleSlot onTurn = OldGameManager.Instance.BattleManager.OnTurnSlot;
        if (!onTurn.IsEmpty && onTurn.ActorData is Ally)
        {
            for (int i = 0; i < onTurn.ActorData.skillDatas.Length; i++)
            {
                BattleSlot[] temp = onTurn.ActorData.skillDatas[i].SetTarget(onTurn, onTurn.ActorData.skillDatas[i].AffectType);
                int count = 0;
                foreach (BattleSlot slot in temp)
                {
                    if (slot != null)
                    {
                        count++;
                    }
                }
                
                if (skills != null)
                {
                    if (count != 0)
                    {
                        guideLines[(skills[i].EffectCount - 1)].color = Color.white;
                    }
                    else
                    {
                        guideLines[(skills[i].EffectCount - 1)].color = unvalidColor;
                    }
                }
            }
        }
    }

    public void GuideReset()
    {
        if (skills != null)
        {
            transform.SetParent(OldGameManager.Instance.SlotVisualizer.transform);
            for (int i = 0; i < guideLines.Length; i++)
            {
                guideLines[i].transform.localPosition = guideDefaultPos;
                guideLines[i].gameObject.SetActive(false);
            }
            skills = null; 
        }
    }

    public void SetInitialGuidePosition()
    {
        if (skills != null)
        {
            float turnX = OldGameManager.Instance.SlotVisualizer.OnTurn.transform.position.x;

            for (int i = 0; i < skills.Length; i++)
            {
                guideLines[i].transform.Translate(new Vector2(skills[i].EffectRange - 2 + turnX, 0));
            }

            transform.SetParent(OldGameManager.Instance.SlotVisualizer.OnTurn.transform); 
        }
    }

    public void OnActivateGuide()
    {
        SetSkill();
        GuideOnOff(true);
        SetInitialGuidePosition();
        GuideAlpha();
    }

    public void OnDeactivateGuide()
    {
        GuideReset();
        GuideOnOff(false);
    }
}
