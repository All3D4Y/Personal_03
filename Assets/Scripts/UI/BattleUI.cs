using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    Button changeBTN;
    Button skillBTN;
    Button itemBTN;
    Button rightBTN;
    Button leftBTN;

    CanvasGroup battleUI;
    CanvasGroup changeGroup;
    CanvasGroup skillGroup;
    CanvasGroup itemGroup;

    void Awake()
    {
        battleUI = GetComponent<CanvasGroup>();

        changeBTN = transform.GetChild(1).GetComponent<Button>();
        skillBTN = transform.GetChild(2).GetComponent<Button>();
        itemBTN = transform.GetChild(3).GetComponent<Button>();
        changeGroup = transform.GetChild(4).GetComponent<CanvasGroup>();
        skillGroup = transform.GetChild(5).GetComponent<CanvasGroup>();
        itemGroup = transform.GetChild(6).GetComponent<CanvasGroup>();
        rightBTN = transform.GetChild(7).GetComponent<Button>();
        leftBTN = transform.GetChild(8).GetComponent<Button>();

        changeBTN.onClick.AddListener(OnChangeGroup);
        skillBTN.onClick.AddListener(OnSkillGroup);
        itemBTN.onClick.AddListener(OnItemGroup);
        rightBTN.onClick.AddListener(OnRightClick);
        leftBTN.onClick.AddListener(OnLeftClick);
    }

    public void Initialize()
    {
        battleUI.alpha = 1.0f;
        battleUI.interactable = true;
        battleUI.blocksRaycasts = true;

        IsNotActionState();
    }

    public void OnChangeGroup()
    {
        changeGroup.alpha = 1;
        skillGroup.alpha = 0;
        itemGroup.alpha = 0;

        changeGroup.interactable = true;
        skillGroup.interactable = false;
        itemGroup.interactable = false;

        changeGroup.blocksRaycasts = true;
        skillGroup.blocksRaycasts = false;
        itemGroup.blocksRaycasts = false;
    }

    public void OnSkillGroup()
    {
        changeGroup.alpha = 0;
        skillGroup.alpha = 1;
        itemGroup.alpha = 0;

        changeGroup.interactable = false;
        skillGroup.interactable = true;
        itemGroup.interactable = false;

        changeGroup.blocksRaycasts = false;
        skillGroup.blocksRaycasts = true;
        itemGroup.blocksRaycasts = false;
    }

    public void OnItemGroup()
    {
        changeGroup.alpha = 0;
        skillGroup.alpha = 0;
        itemGroup.alpha = 1;

        changeGroup.interactable = false;
        skillGroup.interactable = false;
        itemGroup.interactable = true;

        changeGroup.blocksRaycasts = false;
        skillGroup.blocksRaycasts = false;
        itemGroup.blocksRaycasts = true;
    }

    public void IsNotActionState()
    {
        changeGroup.alpha = 0;
        skillGroup.alpha = 0;
        itemGroup.alpha = 0;

        changeGroup.interactable = false;
        skillGroup.interactable = false;
        itemGroup.interactable = false;

        changeGroup.blocksRaycasts = false;
        skillGroup.blocksRaycasts = false;
        itemGroup.blocksRaycasts = false;
    }

    public void OnRightClick()
    {
        GameManager.Instance.BattleManager.BattleInput.onScroll?.Invoke(-1);
    }
    public void OnLeftClick()
    {
        GameManager.Instance.BattleManager.BattleInput.onScroll?.Invoke(1);
    }
}
