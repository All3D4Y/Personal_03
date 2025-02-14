using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    BattleState currentState;               // 현재 상태
    Dictionary<Type, BattleState> states;   // 상태 목록

    public Dictionary<Type, BattleState> States => states;
    public TurnOrder TurnOrder { get; set; }
    public SlotManager PlayerSlot { get; set; }
    public SlotManager EnemySlot { get; set; }
    public List<Character> PlayerParty { get; private set; }
    public List<Character> EnemyParty { get; private set; }

    public Character OnTurnCharacter {  get; private set; }
    public EnemyAction EnemyAction { get; set; }
    public ActionManager ActionManager { get; private set; }
    public OnTurnEffect OnTurnEffect { get; private set; }

    void Awake()
    {
        ActionManager = GetComponent<ActionManager>();
    }

    void Start()
    {
        // 상태 초기화
        states = new Dictionary<Type, BattleState>
        {
            { typeof(Preparation), new Preparation(this) },
            { typeof(SelectAction), new SelectAction(this) },
            { typeof(Execution), new Execution(this) },
            { typeof(StateUpdate), new StateUpdate(this) },
            { typeof(TurnEnd), new TurnEnd(this) },
            { typeof(Conclusion), new Conclusion(this) }
        };

        // 초기 상태 = Preparation
        ChangeState<Preparation>();
    }

    void Update()
    {
        currentState?.Update();
    }

    public void ChangeState<T>() where T : BattleState
    {
        if (currentState != null)
            currentState.Exit();

        currentState = states[typeof(T)];
        currentState.Enter();
    }

    public void ChangeState(Type stateType)
    {
        if (!typeof(BattleState).IsAssignableFrom(stateType))
        {
            Debug.LogError($"Invalid state type: {stateType}");
            return;
        }

        if (currentState != null)
            currentState.Exit();

        currentState = states[stateType];
        currentState.Enter();
    }

    public void SetTurnCharacter(Character character)
    {
        OnTurnCharacter = character;
    }

    public void InitializeBattle()
    {
        // 리스트 초기화
        PlayerParty = new List<Character>();
        EnemyParty = new List<Character>();

        // 슬롯 초기화
        PlayerSlot = new SlotManager(8, true);  // 아군 슬롯 8개
        EnemySlot = new SlotManager(8, false);  // 적 슬롯 8개

        // 아군 배치
        int[] characterCodes = PlayerDataManager.Instance.players;
        CharacterFactory cF = Factory.Instance.CharacterFactory;
        for (int i = 0; i < characterCodes.Length; i++)
        {
            Character temp = cF.GenerateCharacter(characterCodes[i], cF.transform);
            if (temp != null)
            {
                PlayerSlot.AssignCharacterToSlot(temp, i);                                  // 슬롯에 캐릭터 등록
                temp.transform.Translate(PlayerSlot.GetSlot(i).SlotTransform.position);     // 위치 설정
                PlayerParty.Add(temp);                                                      // 플레이어 파티(리스트)에 등록
                temp.onDie += () => PlayerParty.Remove(temp);                               // 죽으면 리스트에서 빠지도록 델리게이트 등록
            }
        }

        // 적 배치
        characterCodes = StageDataManager.Instance.CurrentStage.enemyCodes;
        for (int i = 0; i < characterCodes.Length; i++)
        {
            Character temp = cF.GenerateCharacter(characterCodes[i], cF.transform);
            if (temp != null)
            {
                EnemySlot.AssignCharacterToSlot(temp, i);                                   // 슬롯에 캐릭터 등록
                temp.transform.Translate(EnemySlot.GetSlot(i).SlotTransform.position);      // 위치 설정
                EnemyParty.Add(temp);                                                       // 적 파티(리스트)에 등록
                temp.onDie += () => EnemyParty.Remove(temp);                                // 죽으면 리스트에서 빠지도록 델리게이트 등록
            }
        }

        // 턴 표시 이펙트 찾아두기
        OnTurnEffect = FindAnyObjectByType<OnTurnEffect>();
        OnTurnEffect.OnTransparent();
    }
}
