using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    public bool IsEmpty => item == null;        // UI에 할당된 아이템 정보가 있는지
    public int Count                            // 할당된 아이템의 개수를 확인하는 프로퍼티
    {
        get => count;
        set
        {
            if (count != 0)
            {
                if (value == 0)                 // 0이 되면 아이템 정보 삭제 후 UI 비활성화
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

    /// <summary>
    /// 초기화
    /// </summary>
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

    /// <summary>
    /// UI에 아이템 정보를 할당하는 함수
    /// </summary>
    /// <param name="item">할당할 아이템</param>
    /// <param name="count">아이템의 개수</param>
    public void AssignItem(Item item, int count)
    {
        this.item = item;
        this.count = count;
    }

    /// <summary>
    /// 할당된 아이템 정보를 삭제하는 함수
    /// </summary>
    public void Clear()
    {
        item = null;
        count = 0;
    }

    /// <summary>
    /// 아이템 사용이 유효한지 확인하는 함수
    /// </summary>
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

    /// <summary>
    /// 아이템 정보를 ActionManager에 저장하는 함수, 행동 실행 단계에서 실행
    /// </summary>
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
