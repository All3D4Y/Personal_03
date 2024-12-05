using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldActorStatusGroupUI : MonoBehaviour
{
    OldActorStatusUI[] actorStatusUIs;

    void Awake()
    {
        actorStatusUIs = new OldActorStatusUI[transform.childCount];
        for (int i = 0; i < actorStatusUIs.Length; i++)
        {
            actorStatusUIs[i] = transform.GetChild(i).GetComponent<OldActorStatusUI>();
        }
    }

    public void Initialize()
    {
        int count = 0;
        BattleSlot[] temp = OldGameManager.Instance.SlotController.AllySlot;

        for (int i = 0; i < temp.Length; ++i)
        {
            if (!temp[i].IsEmpty)
            {
                actorStatusUIs[i].Actor = temp[i].ActorData;
                temp[i].ActorData.onHPChanged += actorStatusUIs[i].OnHPChanged;
                temp[i].ActorData.onMPChanged += actorStatusUIs[i].OnMPChanged;
                count++;
            }
        }

        temp = OldGameManager.Instance.SlotController.AllyStandbySlot;
        for (int i = count; i < temp.Length + count; ++i)
        {
            if (!temp[i].IsEmpty)
            {
                actorStatusUIs[i].Actor = temp[i].ActorData;
                temp[i].ActorData.onHPChanged += actorStatusUIs[i].OnHPChanged;
                temp[i].ActorData.onMPChanged += actorStatusUIs[i].OnMPChanged;
                count++;
            }
        }

        temp = OldGameManager.Instance.SlotController.EnemySlot;
        for (int i = count; i < temp.Length + count; ++i)
        {
            if (!temp[i].IsEmpty)
            {
                actorStatusUIs[i].Actor = temp[i].ActorData;
                temp[i].ActorData.onHPChanged += actorStatusUIs[i].OnHPChanged;
                temp[i].ActorData.onMPChanged += actorStatusUIs[i].OnMPChanged;
                count++;
            }
        }

        temp = OldGameManager.Instance.SlotController.EnemyStandbySlot;
        for (int i = count; i < temp.Length + count; ++i)
        {
            if (!temp[i].IsEmpty)
            {
                actorStatusUIs[i].Actor = temp[i].ActorData;
                temp[i].ActorData.onHPChanged += actorStatusUIs[i].OnHPChanged;
                temp[i].ActorData.onMPChanged += actorStatusUIs[i].OnMPChanged;
            }
        }

        for (int i = count; actorStatusUIs.Length > 0; ++i)
        {
            if (actorStatusUIs[i].Actor != null)
            {
                actorStatusUIs[i].gameObject.SetActive(true);
            }
        }
    }
}
