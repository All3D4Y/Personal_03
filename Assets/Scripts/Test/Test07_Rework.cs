using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test07_Rework : TestBase
{
    public BattleManager battleManager;

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        battleManager.ChangeState<Execution>();
    }

}
