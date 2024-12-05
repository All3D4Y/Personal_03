using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchGroupUI : GroupUIBase
{
    SwitchUI[] switchUIs;

    protected override void Awake()
    {
        base.Awake();

        switchUIs = new SwitchUI[3];
        for (int i = 0; i < switchUIs.Length; i++)
        {
            transform.GetChild(i).GetComponent<SwitchUI>();
        }
    }

    public void Initialize()
    {
        foreach (SwitchUI switchUI in switchUIs)
        {
            switchUI.Initialize();
            if (!switchUI.IsEmpty) 
                switchUI.gameObject.SetActive(true);
            else
                switchUI.gameObject.SetActive(false);
        }
    }
}
