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
        // 전투 시 기본 지급
        AddNewItem(egg, 999);
        AddNewItem(heal, 10);
        AddNewItem(mind, 10);
    }

    /// <summary>
    /// 새로운 아이템을 인벤토리에 추가하는 함수
    /// </summary>
    /// <param name="item">얻은 아이템</param>
    /// <param name="count">개 수</param>
    public void AddNewItem(Item item, int count = 1)
    {
        if (!items.ContainsKey(item))   // 인벤토리에 해당 아이템이 존재하지 않으면
            items.Add(item, count);     // 아이템과 개수 추가
        else
            GetItem(item, count);       // 이미 존재하면 개수만 추가
    }

    /// <summary>
    /// 이미 가지고 있는 아이템을 추가로 획득하는 함수
    /// </summary>
    /// <param name="item">얻은 아이템</param>
    /// <param name="count">개 수</param>
    public void GetItem(Item item, int count = 1)
    {
        if (items.ContainsKey(item))    // 인벤토리에 해당 아이템이 있다면
            items[item] += count;       // 개 수 추가
        else
            AddNewItem(item, count);    // 새로 얻은 아이템이면 새 아이템으로 추가
    }

    /// <summary>
    /// 아이템 사용 시 개수를 차감하는 함수, 달걀던지기는 개 수 무한
    /// </summary>
    /// <param name="item">사용한 아이템</param>
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
#if UNITY_EDITOR
                Debug.LogWarning($"{item}의 보유량이 부족해 아이템 사용에 실패했습니다!"); 
#endif
        }
    }
}
