using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public Item egg;

    Dictionary<Item, int> items;

    public Dictionary<Item, int> Items => items;

    void Awake()
    {
        items = new Dictionary<Item, int>();
    }

    void Start()
    {
        AddNewItem(egg, 999);
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
            items[item]--;
            if (items[item] == 0)
            {
                items.Remove(item);
            } 
        }
    }
}
