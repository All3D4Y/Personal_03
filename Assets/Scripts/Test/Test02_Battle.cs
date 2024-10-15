using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test02_Battle : TestBase
{
    TestCharacter character;

    void Start()
    {
        character = FindAnyObjectByType<TestCharacter>();
    }

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        character.ChangeState(CharaterState.BattlePrep);
    }
    protected override void OnTest2(InputAction.CallbackContext context)
    {
        character.ChangeState(CharaterState.SelectAction);
    }
    protected override void OnTest3(InputAction.CallbackContext context)
    {
        character.ChangeState(CharaterState.Battle);
    }
}   