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

    /// <summary>
    /// 초기화
    /// </summary>
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

    /// <summary>
    /// 캐릭터 교체 UI에 할당된 캐릭터 일괄 삭제
    /// </summary>
    public void Clear()
    {
        foreach (SwitchUI switchUI in switchUIs)
        {
            switchUI.Clear();
        }
    }
}
