using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test04_UI : TestBase
{
    public BattleUI bui;
    public SkillUI sui;

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        bui.Initialize();
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        
    }
}
