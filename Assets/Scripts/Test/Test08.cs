using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test08 : TestBase
{
    public Character spum;
    protected override void OnTest1(InputAction.CallbackContext context)
    {
        spum.CharacterAnim.BuffDebuff();
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        spum.CharacterAnim.Hurt();
    }

    protected override void OnTest3(InputAction.CallbackContext context)
    {
        spum.CharacterAnim.GetBuff();
    }
}
