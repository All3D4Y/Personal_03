using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffIconUI : MonoBehaviour
{
    Image[] icons;

    public Image[] Icons => icons;


    /// <summary>
    /// 초기화
    /// </summary>
    public void Initialize()
    {
        icons = new Image[3];

        for (int i = 0; i < icons.Length; i++)
        {
            icons[i] = transform.GetChild(i).GetComponent<Image>();
        }
    }

    /// <summary>
    /// 버프 종류에 따라 버프 아이콘을 표시하는 함수
    /// </summary>
    /// <param name="type">버프 종류</param>
    public void SetIcon(BuffType type)
    {
        switch (type)
        {
            case BuffType.Attack:
                icons[0].enabled = true;
                break;
            case BuffType.Defense:
                icons[1].enabled = true;
                break;
            case BuffType.Speed:
                icons[2].enabled = true;
                break;
        }
    }

    /// <summary>
    /// 아이콘을 끄는 함수
    /// </summary>
    public void ClearIcon()
    {
        foreach (var icon in icons)
        {
            icon.enabled = false;
        }
    }

    /// <summary>
    /// 표시된 아이콘이 하나라도 있는지 확인하는 함수
    /// </summary>
    /// <returns>표시된 아이콘이 있으면 true반환</returns>
    public bool IsActivated()
    {
        bool result = false;

        foreach (var icon in Icons)
        {
            result |= icon.enabled;
        }

        return result;
    }
}
