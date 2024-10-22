using BattlePhase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    Phase phase;

    SlotController slotController;

    TurnCalculator turnCalculator;

    OnFieldCharacter onFieldCharacter;

    BattleInput battleInput;

    StageData stageData = null;

    // Properties
    public Phase Phase => phase;
    public SlotController SlotController => slotController;

    public TurnCalculator TurnCalculator => turnCalculator;

    public BattleInput BattleInput => battleInput;

    public BattleSlot OnTurnSlot { get; private set; }


    void Awake()
    {
        phase = new Phase(this);
        slotController = new SlotController();
        turnCalculator = new TurnCalculator(this.slotController);
        onFieldCharacter = GetComponent<OnFieldCharacter>();
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
    public void SetOnTurnSlot(BattleSlot slot)
    {
        OnTurnSlot = slot;
        Debug.Log($"턴 설정: {OnTurnSlot.EntityData.name}");
    }

    /// <summary>
    /// 차례인 슬롯을 비우는 함수
    /// </summary>
    public void ClearTurnSlot()
    {
        OnTurnSlot = null;
    }

    /// <summary>
    /// 대기석의 캐릭터와 차례인 캐릭터를 교체하는 함수
    /// </summary>
    /// <param name="target"></param>
    public void SwapCharacter(StandbySlot target)
    {
        SlotController.SwapSlot(OnTurnSlot, target);
        SetOnTurnSlot(target);
    }

    // use item, skill, swap slot

    /// <summary>
    /// 행동 중인 슬롯의 위치를 이동시키는 함수
    /// </summary>
    /// <param name="change">인덱스값에 더해질 파라미터</param>
    public void OnMoveSlot(int change)
    {
        if (OnTurnSlot != null)
        {
            if (OnTurnSlot.Type == EntityType.Charater)
            {
                if ((OnTurnSlot.Index > 0 && change == -1) || (OnTurnSlot.Index < 3 && change == 1))
                {
                    SlotController.SwapSlot(OnTurnSlot, SlotController.CharacterSlot[OnTurnSlot.Index + change]);
                    SetOnTurnSlot(SlotController.CharacterSlot[OnTurnSlot.Index + change]);
                }
            }
            else
            {
                if ((OnTurnSlot.Index > 0 && change == -1) || (OnTurnSlot.Index < 3 && change == 1))
                {
                    SlotController.SwapSlot(OnTurnSlot, SlotController.EnemySlot[OnTurnSlot.Index + change]);
                    SetOnTurnSlot(SlotController.EnemySlot[OnTurnSlot.Index + change]);
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
            Debug.Log("스킬 사용");
            action.ActionExecute(OnTurnSlot, action.SetTarget(OnTurnSlot));
        }
        else
        {
            // 아이템 사용
            Debug.Log("아이템 사용");
        }
        Phase.ChangeState(Phase.Battle);
    }

    public void GetDamage(BattleSlot[] targets, float damage)
    {

    }

    /// <summary>
    /// 스테이지 정보를 로드하는 함수
    /// </summary>
    /// <param name="stageIndex">스테이지의 인덱스</param>
    public void LoadStage(uint stageIndex)
    {
        Debug.Log("스테이지 정보 로드");

        CharacterData[] characterDatas = null;
        EnemyDataBase[] enemyDatas = null;

        onFieldCharacter.TestInsert();
        Debug.LogWarning("테스트코드 사용 중임, 변경 필수");
        characterDatas = onFieldCharacter.OnFieldCharacters;

        stageData = GameManager.Instance.StageDataManager[stageIndex];
        enemyDatas = stageData.enemyDatas;

        SlotController.InitialAssign(characterDatas, enemyDatas);
    }

    public void LoadInitSpeed()
    {
        if (!SlotController.CharacterSlot[0].IsEmpty)   // 이 함수가 호출될 때 캐릭터슬롯 맨 앞자리에 아무도 없으면 데이터 로드가 안된거임
        {
            for (uint i = 0; i < SlotController.CharacterSlot.Length; i++)
            {
                // entity들의 Speed += InitialSpeed
                SlotController.CharacterSlot[i].EntityData.Speed += SlotController.CharacterSlot[i].EntityData.InitialSpeed;
                SlotController.EnemySlot[i].EntityData.Speed += SlotController.EnemySlot[i].EntityData.InitialSpeed;
            }
        }
    }
}
