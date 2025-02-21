using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    Button button;
    Image board;
    Image itemIcon;
    TextMeshProUGUI itemTargetCount;
    TextMeshProUGUI itemName;
    TextMeshProUGUI itemDescription;
    TextMeshProUGUI itemCount;

    Item item = null;
    int count = 0;

    Color invalidColor = new Color(1, 1, 1, 0.2f);

    public bool IsEmpty => item == null;
    public int Count
    {
        get => count;
        set
        {
            if (count != 0)
            {
                if (value == 0)
                {
                    Clear();
                    gameObject.SetActive(false);
                }
                else
                    count = value;
            }
        }
    }

    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(UIExecution);

        board = GetComponent<Image>();

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

            IsValidTarget();

            if (!gameObject.activeSelf)
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

    public void IsValidTarget()
    {
        if (!IsEmpty)
        {
            if (item.IsValid())
            {
                board.color = Color.white;
                button.interactable = true;
            }
            else
            {
                board.color = invalidColor;
                button.interactable = false;
            }
        }
    }

    public void UIExecution()
    {
        if (!IsEmpty)
        {
            if (item.ItemID != 0)
            {
                Count--;
                itemCount.text = count.ToString();
            }
            GameManager.Instance.BattleManager.ActionManager.SetAction(item as Skill);
            GameManager.Instance.BattleManager.ChangeState<Execution>();
        }
    }
}
