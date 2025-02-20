using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    Button button;
    Image itemIcon;
    TextMeshProUGUI itemTargetCount;
    TextMeshProUGUI itemName;
    TextMeshProUGUI itemDescription;
    TextMeshProUGUI itemCount;

    Item item = null;
    int count = 0;

    public bool IsEmpty => item == null;
    public int Count
    {
        get => count;
        set
        {
            count = value;
            if (count == 0)
            {
                Clear();
                gameObject.SetActive(false);
            }
        }
    }

    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(UIExecution);

        Transform child;

        child = transform.GetChild(0).GetChild(0);
        itemIcon = child.GetComponent<Image>();

        child = transform.GetChild(1);
        itemTargetCount = child.GetComponent<TextMeshProUGUI>();

        child = transform.GetChild(2);
        itemName = child.GetComponent<TextMeshProUGUI>();

        child = transform.GetChild(3);
        itemDescription = child.GetComponent<TextMeshProUGUI>();

        child = transform.GetChild(5);
        itemCount = child.GetComponent<TextMeshProUGUI>();

        gameObject.SetActive(false);
    }

    public void Initialize()
    {
        if (!IsEmpty)
        {
            itemIcon.sprite = item.icon;
            if (item.Count != 1)
                itemTargetCount.text = "[Multi]";
            else
                itemTargetCount.text = "[Single]";
            itemName.text = item.iS_Name;
            itemDescription.text = item.iS_Description;
            itemCount.text = count.ToString();

            gameObject.SetActive(true);
        }
        else
            gameObject.SetActive(false);
    }

    public void AssignItem(Item item, int count)
    {
        this.item = item;
        this.count = count;
    }

    public void Clear()
    {
        item = null;
        count = 0;
    }

    public void UIExecution()
    {
        if (!IsEmpty)
        {
            Count--;
            itemCount.text = count.ToString();
            GameManager.Instance.BattleManager.ActionManager.SetAction(item as Skill);
            GameManager.Instance.BattleManager.ChangeState<Execution>();
        }
    }
}
