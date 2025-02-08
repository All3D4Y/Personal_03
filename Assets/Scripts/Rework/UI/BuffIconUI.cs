using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffIconUI : MonoBehaviour
{
    List<Image> icons;

    public List<Image> Icons => icons;


    void Awake()
    {
        icons = new List<Image>(3);

        for (int i = 0; i < icons.Count; i++)
        {
            icons[i] = transform.GetChild(i).GetComponent<Image>();
        }
    }

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

    public void ClearIcon()
    {
        foreach (var icon in icons)
        {
            icon.enabled = false;
        }
    }
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
