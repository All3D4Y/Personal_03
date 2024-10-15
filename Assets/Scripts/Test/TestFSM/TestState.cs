using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TestState
{
    public abstract void Enter(TestCharacter character);
    public abstract void Update(TestCharacter character);
    public abstract void Exit(TestCharacter character);
}
