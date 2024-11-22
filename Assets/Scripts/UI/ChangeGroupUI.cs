using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGroupUI : MonoBehaviour
{
    ChangeUI[] changeUIs;

    public void Initialize()
    {
        changeUIs = new ChangeUI[transform.childCount];
        for (int i = 0; i < changeUIs.Length; i++)
        {
            changeUIs[i] = transform.GetChild(0).GetChild(i).GetComponent<ChangeUI>();
        }
    }

    public void OnActivate()
    {
        BattleSlot[] temp = GameManager.Instance.SlotController.AllyStandbySlot;
        for (int i = 0; i < temp.Length; i++)
        {
            if (!temp[i].IsEmpty)
            {
                changeUIs[i].gameObject.SetActive(true);
                changeUIs[i].SetSlot(temp[i]);
                changeUIs[i].GetData();
            }
        }
    }

    public void OnDeactivate()
    {
        for (int i = 0; i < changeUIs.Length; i++)
        {
            changeUIs[i].gameObject.SetActive(false);
            changeUIs[i].ClearData();
        }
    }
}
