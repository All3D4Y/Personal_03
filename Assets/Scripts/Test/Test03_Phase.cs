using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class Test03_Phase : TestBase
{
    BattleManager battleManager;
    Ally[] characterDatas;
    Enemy[] enemyDatas;
    public SlotVisualizer slotVisualizer;
    public Transform a;
    public Transform b;


    void Start()
    {
        battleManager = GameManager.Instance.BattleManager;
    }

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        battleManager.UseSkillOrItem(battleManager.OnTurnSlot.ActorData.skillDatas[0]);
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        battleManager.UseSkillOrItem(battleManager.OnTurnSlot.ActorData.skillDatas[1]);
    }

    protected override void OnTest3(InputAction.CallbackContext context)
    {
        //slotVisualizer.TestSwap();
    }

    protected override void OnTest4(InputAction.CallbackContext context)
    {
        battleManager.SlotController.TestPrint();
    }

    protected override void OnTest5(InputAction.CallbackContext context)
    {
        battleManager.TestTrigger();
    }
}
