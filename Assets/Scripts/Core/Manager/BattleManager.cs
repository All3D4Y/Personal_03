using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    BattleState currentState;               // 현재 상태
    Dictionary<Type, BattleState> states;   // 상태 목록

    public TurnOrder TurnOrder { get; set; }                    // 턴 계산용
    public SlotManager PlayerSlot { get; set; }                 // 플레이어 슬롯
    public SlotManager EnemySlot { get; set; }                  // 적 슬롯
    public List<Character> PlayerParty { get; private set; }    // 플레이어 캐릭터의 리스트
    public List<Character> EnemyParty { get; private set; }     // 적 캐릭터의 리스트
                                                                
    public Character OnTurnCharacter {  get; private set; }     // 현재 차례인 캐릭터
    public EnemyAction EnemyAction { get; set; }                // 적 행동 로직
    public ActionManager ActionManager { get; private set; }    // 실행할 행동을 저장하고 실행하는 클래스
    public OnTurnEffect OnTurnEffect { get; private set; }      // 현재 차례를 표시하는 이펙트

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
        // 현재 상태의 Update함수를 여기서 실행
        currentState?.Update();
    }

    /// <summary>
    /// 상태를 변경하는 함수, BattleState를 상속받은 제네릭 타입만 가능
    /// </summary>
    /// <typeparam name="T">변경할 상태</typeparam>
    public void ChangeState<T>() where T : BattleState
    {
        if (currentState != null)
            currentState.Exit();

        currentState = states[typeof(T)];
        currentState.Enter();
    }

    /// <summary>
    /// 상태를 변경하는 함수
    /// </summary>
    /// <param name="stateType">변경할 상태</param>
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

    /// <summary>
    /// 현재 차례인 캐릭터를 설정하는 함수
    /// </summary>
    /// <param name="character"></param>
    public void SetTurnCharacter(Character character)
    {
        OnTurnCharacter = character;
    }

    /// <summary>
    /// 전투 시작 시 초기화 함수
    /// </summary>
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
                temp.Level = PlayerDataManager.Instance.playerLevel;
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
                temp.Level = StageDataManager.Instance.CurrentStage.enemyLevel;
                EnemySlot.AssignCharacterToSlot(temp, i);                                   // 슬롯에 캐릭터 등록
                temp.transform.Translate(EnemySlot.GetSlot(i).SlotTransform.position);      // 위치 설정
                EnemyParty.Add(temp);                                                       // 적 파티(리스트)에 등록
                temp.onDie += () => EnemyParty.Remove(temp);                                // 죽으면 리스트에서 빠지도록 델리게이트 등록
            }
        }

        // 턴 표시 이펙트 찾아두고 일단 안보이게 설정
        OnTurnEffect = FindAnyObjectByType<OnTurnEffect>();
        OnTurnEffect.OnTransparent();
    }
}
