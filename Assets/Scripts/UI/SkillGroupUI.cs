using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillGroupUI : MonoBehaviour
{
    SkillUI[] skillUIs;

    Ally onTurnAlly = null;

    public void Initialize()
    {
        onTurnAlly = GameManager.Instance.BattleManager.OnTurnSlot.ActorData as Ally;

        skillUIs = new SkillUI[4];
        for (int i = 0; i < 4; i++)
        {
            skillUIs[i] = transform.GetChild(0).GetChild(i).GetComponent<SkillUI>();
        }

        if (onTurnAlly != null)
        {
            for (int i = 0; i < onTurnAlly.skillDatas.Length; i++)
            {
                skillUIs[i].Initialize();
                skillUIs[i].SetSkill(onTurnAlly, i);
                skillUIs[i].gameObject.SetActive(!skillUIs[i].IsEmpty);
                skillUIs[i].OnOffGuide(true);
                GameManager.Instance.BattleManager.BattleInput.onScroll += skillUIs[i].OnMoveGuide;
            }
        }
    }

    public void OnReset()
    {
        if (onTurnAlly != null)
        {
            for (int i = 0; i < onTurnAlly.skillDatas.Length; i++)
            {
                GameManager.Instance.BattleManager.BattleInput.onScroll -= skillUIs[i].OnMoveGuide;
                skillUIs[i].OnOffGuide(false);
                skillUIs[i].Clear();
                skillUIs[i].gameObject.SetActive(false);
            }
        }
        onTurnAlly = null;
    }
}
