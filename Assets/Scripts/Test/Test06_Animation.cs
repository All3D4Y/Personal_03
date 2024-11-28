using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test06_Animation : TestBase
{
    public Ally testAlly_0;
    public Ally testAlly_1;
    public Enemy enemy_0;
    public Enemy enemy_1;

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        testAlly_0.AttackAnimation(0);
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        testAlly_1.AttackAnimation(2);
    }

    protected override void OnTest3(InputAction.CallbackContext context)
    {
        enemy_0.AttackAnimation(0);
    }

    protected override void OnTest4(InputAction.CallbackContext context)
    {
        enemy_1.AttackAnimation(1);
    }
}
