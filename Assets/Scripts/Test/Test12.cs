using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test12 : TestBase
{
    public Character character;
    public Transform trans;

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        GameManager.Instance.CoroutineManager.OnMoveCharacter(character, trans.position);
    }
}
