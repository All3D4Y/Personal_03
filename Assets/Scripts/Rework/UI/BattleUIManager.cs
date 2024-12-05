using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUIManager : GroupUIBase
{
    Button switchBTN;
    Button skillBTN;
    Button itemBTN;
    Button rightBTN;
    Button leftBTN;

    SwitchGroupUI switchGroupUI;
    SkillGroupUI skillGroupUI;
    //ItemGroupUI itemUI;

    public Action<int> onMoveInput;

    public SwitchGroupUI SwitchUIs => switchGroupUI;
    public SkillGroupUI SkillUIs => skillGroupUI;

    protected override void Awake()
    {
        base.Awake();

        switchGroupUI = transform.GetChild(4).GetComponent<SwitchGroupUI>();
        skillGroupUI = transform.GetChild(5).GetComponent<SkillGroupUI>();
        //itemGroupUI

        switchBTN = transform.GetChild(1).GetComponent<Button>();
        skillBTN = transform.GetChild(2).GetComponent<Button>();
        itemBTN = transform.GetChild(3).GetComponent<Button>();
        rightBTN = transform.GetChild(7).GetComponent<Button>();
        leftBTN = transform.GetChild(8).GetComponent<Button>();

        switchBTN.onClick.AddListener(OnSwitch);
        skillBTN.onClick.AddListener(OnSkill);
        itemBTN.onClick.AddListener(OnItem);
        rightBTN.onClick.AddListener(OnRightClick);
        leftBTN.onClick.AddListener(OnLeftClick);
    }

    public void PreInitialize()
    {
        // preparation 단계에서 최초 1회만 실행
    }

    public void Initialize()
    {
        // selectAction 단계의 enter에서 실행
    }

    void OnSwitch()
    {
        switchGroupUI.OnVisible();
        skillGroupUI.OnTransparent();
        //item.ontransparent
    }

    void OnSkill()
    {
        switchGroupUI.OnTransparent();
        skillGroupUI.OnVisible();
        //item.ontransparent
    }

    void OnItem()
    {
        switchGroupUI.OnTransparent();
        skillGroupUI.OnTransparent();
    }

    void OnRightClick()
    {
        onMoveInput?.Invoke(-1);
    }

    void OnLeftClick()
    {
        onMoveInput?.Invoke(1);
    }
}
