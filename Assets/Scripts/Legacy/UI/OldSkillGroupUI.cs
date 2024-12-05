using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldSkillGroupUI : MonoBehaviour
{
    OldSkillUI[] skillUIs;

    Ally onTurnAlly = null;

    public void Initialize()
    {
        onTurnAlly = OldGameManager.Instance.BattleManager.OnTurnSlot.ActorData as Ally;

        skillUIs = new OldSkillUI[4];
        for (int i = 0; i < 4; i++)
        {
            skillUIs[i] = transform.GetChild(0).GetChild(i).GetComponent<OldSkillUI>();
        }

        if (onTurnAlly != null)
        {
            for (int i = 0; i < onTurnAlly.skillDatas.Length; i++)
            {
                skillUIs[i].Initialize();
                skillUIs[i].SetSkill(onTurnAlly, i);
                skillUIs[i].gameObject.SetActive(!skillUIs[i].IsEmpty);
            }
        }
    }

    public void OnReset()
    {
        if (onTurnAlly != null)
        {
            for (int i = 0; i < onTurnAlly.skillDatas.Length; i++)
            {
                skillUIs[i].Clear();
                skillUIs[i].gameObject.SetActive(false);
            }
        }
        onTurnAlly = null;
    }
}
