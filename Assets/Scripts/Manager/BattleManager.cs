using BattlePhase;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public bool Test { get; private set; }

    PhaseStateMachine phase;

    SlotController slotController;

    TurnCalculator turnCalculator;

    BattleInput battleInput;

    StageData stageData = null;

    public Action<ActorSide, uint> onTurnSet;

    // Properties
    public PhaseStateMachine Phase => phase;
    public SlotController SlotController => slotController;

    public TurnCalculator TurnCalculator => turnCalculator;

    public BattleInput BattleInput => battleInput;

    public BattleSlot OnTurnSlot { get; private set; }


    void Awake()
    {
        phase = new PhaseStateMachine(this);
        slotController = new SlotController();
        turnCalculator = new TurnCalculator(this);
        battleInput = GetComponent<BattleInput>();
    }

    void Start()
    {
        phase.Initialize(phase.Enter);
    }

    void Update()
    {
        phase.Execute();
    }

    /// <summary>
    /// 행동할 차례인 슬롯을 설정하는 함수
    /// </summary>
    /// <param name="slot">행동할 차례인 슬롯</param>
    public void SetTurnSlot(BattleSlot slot)
    {
        OnTurnSlot = slot;
        onTurnSet?.Invoke(slot.Side, slot.Index);
        Debug.Log($"턴 설정: {OnTurnSlot.ActorData.name}");
    }

    /// <summary>
    /// 차례인 슬롯을 비우는 함수
    /// </summary>
    public void ClearTurnSlot()
    {
        onTurnSet = null;
        OnTurnSlot = null;
    }

    /// <summary>
    /// 대기석의 캐릭터와 차례인 캐릭터를 교체하는 함수
    /// </summary>
    /// <param name="target"></param>
    public void SwapCharacter(StandbySlot target)
    {
        SlotController.SwapSlot(OnTurnSlot, target);
        SetTurnSlot(target);
    }

    /// <summary>
    /// 행동 중인 슬롯의 위치를 이동시키는 함수
    /// </summary>
    /// <param name="input">인덱스값에 더해질 파라미터</param>
    public void OnMoveSlot(int input)
    {
        if (OnTurnSlot != null)
        {
            if (OnTurnSlot.Side == ActorSide.Ally)
            {
                if ((OnTurnSlot.Index > 0 && input == -1) || (OnTurnSlot.Index < 3 && input == 1))
                {
                    SlotController.SwapSlot(OnTurnSlot, SlotController.AllySlot[OnTurnSlot.Index + input]);
                    SetTurnSlot(SlotController.AllySlot[OnTurnSlot.Index + input]);
                }
            }
            else
            {
                if ((OnTurnSlot.Index > 0 && input == -1) || (OnTurnSlot.Index < 3 && input == 1))
                {
                    SlotController.SwapSlot(OnTurnSlot, SlotController.EnemySlot[OnTurnSlot.Index + input]);
                    SetTurnSlot(SlotController.EnemySlot[OnTurnSlot.Index + input]);
                }
            }
        }
    }
    
    /// <summary>
    /// 스킬이나 아이템을 사용하는 함수
    /// </summary>
    /// <param name="action">선택한 행동</param>
    public void UseSkillOrItem(IAction action)
    {
        if (action is SkillData)
        {
            // 스킬사용
            if (!OnTurnSlot.IsEmpty)
            {
                Debug.Log("스킬 사용");
                action.ActionExecute(OnTurnSlot, action.SetTarget(OnTurnSlot, action.AffectType));
            }
        }
        else
        {
            if (!OnTurnSlot.IsEmpty)
            {
                // 아이템 사용
                Debug.Log("아이템 사용");
            }
        }
            Phase.ChangeState(Phase.Battle);
    }

    public void TurnCount()
    {
        // 버프 유지 카운트 --
        IBuffDebuff[] buffs = BuffDebuffContainer.BuffDebuffs.ToArray();
        foreach (var buff in buffs)
        {
            buff.Duration--;
        }
    }

    public void BattleOver(bool isWin = true)
    {
        if (isWin)
        {
            // 이겼을 때
        }
        else
        {
            // 졌을 때
        }
    }


#if UNITY_EDITOR
    public void TestTrigger()
    {
        if (!Test)
            Test = true;
        else
            Test = false;
    }
#endif
}
