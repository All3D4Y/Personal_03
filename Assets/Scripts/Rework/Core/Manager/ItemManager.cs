using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{
    public Item egg;
    public Item heal;
    public Item mind;

    Dictionary<Item, int> items;

    public Dictionary<Item, int> Items => items;

    void Awake()
    {
        items = new Dictionary<Item, int>();
    }

    void Start()
    {
        AddNewItem(egg, 999);
        AddNewItem(heal, 10);
        AddNewItem(mind, 10);
    }

    public void AddNewItem(Item item, int count = 1)
    {
        if (!items.ContainsKey(item))
            items.Add(item, count);
        else
            GetItem(item, count);
    }

    public void GetItem(Item item, int count = 1)
    {
        if (items.ContainsKey(item))
            items[item] += count;
        else
            AddNewItem(item, count);
    }

    public void UseItem(Item item)
    {
        if (item != egg)
        {
            if (items[item] > 0)
            {
                items[item]--;
                if (items[item] == 0)
                {
                    items.Remove(item);
                }
            }
            else
                Debug.LogWarning($"{item}의 보유량이 부족해 아이템 사용에 실패했습니다!");
        }
    }
}
