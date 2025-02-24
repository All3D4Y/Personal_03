using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpoilsUI : MonoBehaviour
{
    Image icon;
    TextMeshProUGUI newText;
    TextMeshProUGUI countText;

    Item item = null;
    int count = 0;

    void Awake()
    {
        Transform child;

        child = transform.GetChild(1);
        icon = child.GetComponent<Image>();
        newText = child.GetChild(0).GetComponent<TextMeshProUGUI>();

        child = transform.GetChild(3);
        countText = child.GetComponent<TextMeshProUGUI>();
    }

    public void AssignItem(Item item, int count)
    {
        this.item = item;
        this.count = count;
    }

    public void Initialize()
    {
        if (item != null && count > 0)
        {
            icon.sprite = item.icon;
            if (ItemManager.Instance.Items.ContainsKey(item))
                newText.enabled = false;
            else
                newText.enabled = true;
            countText.text = count.ToString(); 
        }
    }
}
