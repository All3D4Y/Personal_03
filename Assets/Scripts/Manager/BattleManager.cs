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

    StageData stageData = null;


    public Phase Phase => phase;
    public SlotController SlotController => slotController;

    public TurnCalculator TurnCalculator => turnCalculator;

    public BattleSlot OnTurnSlot { get; set; }

    void Awake()
    {
        phase = new Phase(this);
        slotController = new SlotController();
        turnCalculator = new TurnCalculator(this.slotController);
        onFieldCharacter = GetComponent<OnFieldCharacter>();
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
    /// 행동 중인 슬롯의 위치를 오른쪽으로 이동하는 함수
    /// </summary>
    public void OnMoveRight()
    {
        if (OnTurnSlot != null)
        {
            if (OnTurnSlot.Type == EntityType.Charater)
            {
                if (OnTurnSlot.Index > 0)
                {
                    slotController.SwapSlot(OnTurnSlot, slotController.CharacterSlot[OnTurnSlot.Index - 1]);
                    SetOnTurnSlot(slotController.CharacterSlot[OnTurnSlot.Index - 1]);
                    Debug.Log("오른쪽으로 이동");
                }
            }
            else
            {
                if (OnTurnSlot.Index > 0)
                {
                    slotController.SwapSlot(OnTurnSlot, slotController.EnemySlot[OnTurnSlot.Index - 1]);
                    SetOnTurnSlot(slotController.EnemySlot[OnTurnSlot.Index - 1]);
                    Debug.Log("왼쪽으로 이동");
                }
            }
        }
    }

    /// <summary>
    /// 행동 중인 슬롯의 위치를 왼쪽으로 이동하는 함수
    /// </summary>
    public void OnMoveLeft()
    {
        if (OnTurnSlot != null)
        {
            if (OnTurnSlot.Type == EntityType.Charater)
            {
                if (OnTurnSlot.Index < 3)
                {
                    slotController.SwapSlot(OnTurnSlot, slotController.CharacterSlot[OnTurnSlot.Index + 1]);
                    SetOnTurnSlot(slotController.CharacterSlot[OnTurnSlot.Index + 1]);
                }
            }
            else
            {
                if (OnTurnSlot.Index < 3)
                {
                    slotController.SwapSlot(OnTurnSlot, slotController.EnemySlot[OnTurnSlot.Index + 1]);
                    SetOnTurnSlot(slotController.EnemySlot[OnTurnSlot.Index + 1]);
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
