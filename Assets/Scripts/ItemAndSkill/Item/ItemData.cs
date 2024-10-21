using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : ItemSkillData
{
    [Header("아이템 정보")]
    public string itemName = "아이템 이름";
    public int itemCount = 0;
    public int itemPrice = 0;
    public ItemType type;
    [TextArea(2, 5)]
    public string itemDescription = "아이템 설명";

    public int ItemCount
    {
        get => itemCount;
        set
        {
            itemCount = Mathf.Clamp(value, 0, 99);
        }
    }

    public int ItemPrice
    {
        get => ItemPrice;
        set => ItemPrice = Mathf.Clamp(value, 0, itemPrice);
    }

    public bool IsActive
    {
        get => itemCount > 0;
    }
}
