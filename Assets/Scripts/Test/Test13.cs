using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test13 : TestBase
{
    public CharacterAnim anim;


    protected override void OnTest1(InputAction.CallbackContext context)
    {
        anim.TestRangedAttack();
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        Factory.Instance.GetArrow(transform.position, false);
    }
}
