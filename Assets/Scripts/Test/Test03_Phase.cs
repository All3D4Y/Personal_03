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
    public Transform pos;


    void Start()
    {
        battleManager = GameManager.Instance.BattleManager;
    }

    protected override void OnTestLClick(InputAction.CallbackContext context)
    {
        Factory.Instance.GetActor(0);
    }
    protected override void OnTest1(InputAction.CallbackContext context)
    {
        Factory.Instance.GetActor(1);
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        Factory.Instance.GetActor(2);
    }

    protected override void OnTest3(InputAction.CallbackContext context)
    {
        Factory.Instance.GetActor(3);
    }

    protected override void OnTest4(InputAction.CallbackContext context)
    {
        Factory.Instance.GetActor(4);
    }

    protected override void OnTest5(InputAction.CallbackContext context)
    {
        Factory.Instance.GetActor(5);
    }
}
