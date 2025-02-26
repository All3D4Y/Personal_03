using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUIManager : GroupUIBase
{
    bool canClick = true;

    Button switchBTN;
    Button skillBTN;
    Button itemBTN;
    Button rightBTN;
    Button leftBTN;

    SwitchGroupUI switchGroupUI;
    SkillGroupUI skillGroupUI;
    ItemGroupUI itemGroupUI;
    CharacterStatusGroupUI characterStatusGroupUI;
    BattleEndUI battleEndUI;

    public Action<int> onMoveInput;

    public SwitchGroupUI SwitchUIs => switchGroupUI;
    public SkillGroupUI SkillUIs => skillGroupUI;
    public CharacterStatusGroupUI CharacterUIs => characterStatusGroupUI;
    public BattleEndUI BattleEndUI => battleEndUI;




    protected override void Awake()
    {
        base.Awake();

        characterStatusGroupUI = transform.parent.GetChild(0).GetComponent<CharacterStatusGroupUI>();

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

        battleEndUI = transform.parent.GetChild(2).GetComponent<BattleEndUI>();
    }

    public void PreInitialize()
    {
        // Preparation 단계에서 최초 1회만 실행
        // Switch쪽은 여기서 등록하기
        SlotManager playerSlot = GameManager.Instance.BattleManager.PlayerSlot;
        
        if (playerSlot != null && !playerSlot.GetSlot(4).IsEmpty)         // 플레이어 슬롯이 있고, 슬롯에 5명 이상이 있을 때
        {
            for (int i = 0; i < 4; i++)
            {
                if (!playerSlot.GetSlot(i + 4).IsEmpty)
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
            skillGroupUI.Initialize();          // 스킬UI 초기화 실행
            itemGroupUI.Initialize();           // 아이템UI 초기화
        }
        else
            Debug.LogWarning("차례인 캐릭터가 없습니다!");

        onMoveInput += GameManager.Instance.BattleManager.PlayerSlot.OnMoveSlot;

        // 액션 선택 단계 진입 시 첫 활성화는 Skill
        OnSkill();
        // 마지막으로 보여지게 하기
        OnVisible();
    }

    public void Clear()
    {
        onMoveInput -= GameManager.Instance.BattleManager.PlayerSlot.OnMoveSlot;
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
        itemGroupUI.OnVisible();
    }

    public void OnBattleEnd()
    {
        OnTransparent();
        battleEndUI.OnVisible();
    }

    void OnRightClick()
    {
        if (canClick)
        {
            canClick = false;
            StartCoroutine(ClickCoolDown());
            onMoveInput?.Invoke(-1);
            IsValidTarget();
        }
    }

    void OnLeftClick()
    {
        if (canClick)
        {
            canClick = false;
            StartCoroutine(ClickCoolDown());
            onMoveInput?.Invoke(1);
            IsValidTarget();
        }
    }

    void IsValidTarget()
    {
        if (skillGroupUI.CanvasGroup.alpha > 0.99f)
            skillGroupUI.IsValidTarget();
        else
            itemGroupUI.IsValidTarget();
    }

    IEnumerator ClickCoolDown()
    {
        float cooldown = 0.5f;
        float elapsedTime = 0.0f;
        try
        {
            while (elapsedTime > cooldown)
            {
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
        finally
        {
            canClick = true;
        }
    }
}
