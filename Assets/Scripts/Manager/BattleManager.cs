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


    public Phase Phase => phase;
    public SlotController SlotController => slotController;

    public TurnCalculator TurnCalculator => turnCalculator;

    public BattleInput BattleInput => battleInput;

    public BattleSlot OnTurnSlot { get; set; }


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
                if (OnTurnSlot.Index > 0 && OnTurnSlot.Index < 3)
                {
                    slotController.SwapSlot(OnTurnSlot, slotController.CharacterSlot[OnTurnSlot.Index + change]);
                    SetOnTurnSlot(slotController.CharacterSlot[OnTurnSlot.Index + change]);
                }
            }
            else
            {
                if (OnTurnSlot.Index > 0 && OnTurnSlot.Index < 3)
                {
                    slotController.SwapSlot(OnTurnSlot, slotController.EnemySlot[OnTurnSlot.Index + change]);
                    SetOnTurnSlot(slotController.EnemySlot[OnTurnSlot.Index + change]);
                }
            }
        }
    }
    public void UseItem()
    {
        Debug.Log("아이템 사용");
        Phase.ChangeState(Phase.Battle);
    }

    public void UseSkill(IAction action)
    {
        Debug.Log("스킬 사용");
        Phase.ChangeState(Phase.Battle);
    }

    public void LoadStage(uint stageIndex)
    {
        Debug.Log("스테이지 정보 로드");

        CharacterData[] characterDatas = null;
        EnemyDataBase[] enemyDatas = null;

        onFieldCharacter.TestInsert();
        characterDatas = onFieldCharacter.OnFieldCharacters;

        stageData = GameManager.Instance.StageDataManager[stageIndex];
        enemyDatas = stageData.enemyDatas;

        SlotController.InitialAssign(characterDatas, enemyDatas);
    }
}
