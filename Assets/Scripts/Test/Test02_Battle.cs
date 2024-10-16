using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test02_Battle : TestBase
{
    public EntityData[] datas;

    [Range(0, 10)]
    public int modifier = 0;

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        
    }
}   