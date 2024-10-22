using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class Test03_Phase : TestBase
{
    BattleManager battleManager;
    CharacterData[] characterDatas;
    EnemyDataBase[] enemyDatas;


    public Action<IAction> onAction;

    void Start()
    {
        battleManager = GameManager.Instance.BattleManager;
    }

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        battleManager.SlotController.TestPrint();
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        battleManager.OnMoveSlot(1);
    }

    protected override void OnTest3(InputAction.CallbackContext context)
    {
        battleManager.OnMoveSlot(-1);
    }
}
