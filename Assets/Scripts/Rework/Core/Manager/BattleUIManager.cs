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
    ItemGroupUI itemGroupUI;

    public Action<int> onMoveInput;

    public SwitchGroupUI SwitchUIs => switchGroupUI;
    public SkillGroupUI SkillUIs => skillGroupUI;


    protected override void Awake()
    {
        base.Awake();

        switchGroupUI = transform.GetChild(4).GetComponent<SwitchGroupUI>();
        skillGroupUI = transform.GetChild(5).GetComponent<SkillGroupUI>();
        itemGroupUI = transform.GetChild(6).GetComponent<ItemGroupUI>();

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
        // Preparation 단계에서 최초 1회만 실행
        // Switch쪽은 여기서 등록하기
        SlotManager playerSlot = GameManager.Instance.BattleManager.PlayerSlot;
        
        if (playerSlot != null && playerSlot.SlotCount > 4)         // 플레이어 슬롯이 있고, 슬롯에 5명 이상이 있을 때
        {
            for (int i = 0; i < playerSlot.SlotCount - 4; i++)
            {
                switchGroupUI.SwitchUIs[i].AssignCharacter(playerSlot.GetSlot(i + 4).CharacterData);
            }
        }
        
        switchGroupUI.Initialize();
    }

    public void Initialize()
    {
        // SelectAction 단계의 Enter에서 실행
        // Turn 정보 받아와서 SkillUI들에 적용하기
        Character turn = GameManager.Instance.BattleManager.OnTurnCharacter;

        if (turn != null)
        {
            skillGroupUI.AssignSkills(turn);    // 캐릭터의 스킬들을 등록하고
            skillGroupUI.Initialize();          // 초기화 실행
        }
        else
            Debug.LogWarning("차례인 캐릭터가 없습니다!");

        // 액션 선택 단계 진입 시 첫 활성화는 Skill
        OnSkill();
        // 마지막으로 보여지게 하기
        OnVisible();
    }

    public void Clear()
    {
        skillGroupUI.Clear();
    }

    void OnSwitch()
    {
        switchGroupUI.OnVisible();
        skillGroupUI.OnTransparent();
        itemGroupUI.OnTransparent();
    }

    void OnSkill()
    {
        switchGroupUI.OnTransparent();
        skillGroupUI.OnVisible();
        itemGroupUI.OnTransparent();
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
