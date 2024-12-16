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
        ui.Initialize(spum);
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        spum.HP -= 6;
    }

    protected override void OnTest3(InputAction.CallbackContext context)
    {
        spum.HP += 6;
    }
}
