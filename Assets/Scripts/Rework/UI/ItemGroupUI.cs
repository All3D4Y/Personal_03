using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ItemGroupUI : GroupUIBase
{
    ItemUI[] itemUIs;

    public void Initialize()
    {
        ItemManager manager = ItemManager.Instance;
        itemUIs = new ItemUI[manager.Items.Count];
        List<Item> list = manager.Items.Keys.ToList();
        list.OrderBy(i => i.ItemID);

        for (int i = 0; i < itemUIs.Length; i++)
        {
            itemUIs[i] = transform.GetChild(i).GetComponent<ItemUI>();
            itemUIs[i].AssignItem(list[i], manager.Items[list[i]]);
            itemUIs[i].Initialize();
        }
    }
}
