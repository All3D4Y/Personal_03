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
}
