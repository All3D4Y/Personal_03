using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchGroupUI : GroupUIBase
{
    SwitchUI[] switchUIs;

    public SwitchUI[] SwitchUIs => switchUIs;

    protected override void Awake()
    {
        base.Awake();

        switchUIs = new SwitchUI[transform.childCount];
        for (int i = 0; i < switchUIs.Length; i++)
        {
            switchUIs[i] = transform.GetChild(i).GetComponent<SwitchUI>();
        }
    }

    public void Initialize()
    {
        foreach (SwitchUI switchUI in switchUIs)
        {
            if (!switchUI.IsEmpty)
            {
                switchUI.Initialize();
                switchUI.gameObject.SetActive(true);
            }
            else
                switchUI.gameObject.SetActive(false);
        }
    }

    public void Clear()
    {
        foreach (SwitchUI switchUI in switchUIs)
        {
            switchUI.Clear();
        }
    }
}
