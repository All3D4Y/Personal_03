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

    /// <summary>
    /// 전리품 UI에 아이템을 등록하는 함수
    /// </summary>
    /// <param name="item">등록할 아이템</param>
    /// <param name="count">아이템의 개 수</param>
    public void AssignItem(Item item, int count)
    {
        this.item = item;
        this.count = count;
    }

    /// <summary>
    /// 초기화
    /// </summary>
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
