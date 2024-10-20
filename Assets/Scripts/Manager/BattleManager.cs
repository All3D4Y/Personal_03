using BattlePhase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    Phase phase;

    SlotController slotController;

    TurnCalculator turnCalculator;

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
    }

    void Start()
    {
        phase.Initialize(phase.Enter);
    }

    void Update()
    {
        phase.Execute();
    }

    public void OnTurn()
    {
        OnTurnSlot = TurnCalculator.GetTurnSlot();
    }

    // use item, skill, swap slot

    public void OnMoveRight()
    {
        if (OnTurnSlot != null)
        {
            if (OnTurnSlot.Type == EntityType.Charater)
            {
                if (OnTurnSlot.Index > 0)
                {
                    slotController.SwapSlot(OnTurnSlot, slotController.CharacterSlot[OnTurnSlot.Index - 1]);
                }
            }
            else
            {
                if (OnTurnSlot.Index > 0)
                {
                    slotController.SwapSlot(OnTurnSlot, slotController.EnemySlot[OnTurnSlot.Index - 1]);
                }
            }
        }
    }

    public void OnMoveLeft()
    {
        if (OnTurnSlot != null)
        {
            if (OnTurnSlot.Type == EntityType.Charater)
            {
                if (OnTurnSlot.Index < 3)
                {
                    slotController.SwapSlot(OnTurnSlot, slotController.CharacterSlot[OnTurnSlot.Index + 1]);
                }
            }
            else
            {
                if (OnTurnSlot.Index < 3)
                {
                    slotController.SwapSlot(OnTurnSlot, slotController.EnemySlot[OnTurnSlot.Index + 1]);
                }
            }
        }
    }
}
