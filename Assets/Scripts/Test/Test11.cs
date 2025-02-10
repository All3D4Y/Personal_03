using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test11 : TestBase
{
    public int damageAmount;
    public Transform trans;

    Vector2 position;

    void Start()
    {
        position = trans.position;
    }


    protected override void OnTest1(InputAction.CallbackContext context)
    {
        position = trans.position;
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
    }
}
