using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test08 : TestBase
{
    public Character spum;

    public CharacterStatusUI ui;
    protected override void OnTest1(InputAction.CallbackContext context)
    {
        spum.Initialize();
        ui.Initialize(spum);
        Debug.Log($"HP:{spum.HP}");
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        spum.HP -= 6;
        Debug.Log($"HP:{spum.HP}");
    }

    protected override void OnTest3(InputAction.CallbackContext context)
    {
        spum.HP += 6;
        Debug.Log($"HP:{spum.HP}");
    }

    protected override void OnTest4(InputAction.CallbackContext context)
    {
        ui.TransformUpdate();
    }
}
