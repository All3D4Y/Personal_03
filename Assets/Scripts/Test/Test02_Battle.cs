using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test02_Battle : TestBase
{
    SlotController controller;

    Ally[] characterDatas;
    OldEnemy[] enemyDatas;

    void Start()
    {
    }

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        controller.TestPrint();
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        controller.AssignSlot(characterDatas[0], 2);
        controller.AssignSlot(characterDatas[1], 3);
        controller.AssignSlot(enemyDatas[1], 2);
        controller.AssignSlot(characterDatas[3], 2, true);
    }

    protected override void OnTest3(InputAction.CallbackContext context)
    {
        controller.InitialAssign(characterDatas, enemyDatas);
    }

    protected override void OnTest4(InputAction.CallbackContext context)
    {
        controller.ClearAllSlot();
    }

    protected override void OnTest5(InputAction.CallbackContext context)
    {
        controller.SortAllSlot();
    }
}   