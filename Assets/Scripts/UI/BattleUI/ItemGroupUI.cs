using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ItemGroupUI : GroupUIBase
{
    ItemUI[] itemUIs;

    /// <summary>
    /// 초기화
    /// </summary>
    public void Initialize()
    {
        ItemManager manager = ItemManager.Instance;
        itemUIs = new ItemUI[transform.childCount];
        List<Item> list = manager.Items.Keys.ToList();
        list.OrderBy(i => i.ItemID);

        for (int i = 0; i < itemUIs.Length; i++)
        {
            itemUIs[i] = transform.GetChild(i).GetComponent<ItemUI>();
            if (i < list.Count)
            {
                itemUIs[i].AssignItem(list[i], manager.Items[list[i]]); 
            }
            itemUIs[i].Initialize();
        }
    }

    /// <summary>
    /// UI에 할당된 아이템 사용이 유효한지 일괄 확인
    /// </summary>
    public void IsValidTarget()
    {
        foreach (var item in itemUIs)
        {
            item.IsValidTarget();
        }
    }
}
