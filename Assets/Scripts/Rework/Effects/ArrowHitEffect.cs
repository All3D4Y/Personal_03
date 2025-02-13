using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowHitEffect : RecycleObject
{
    protected override void OnEnable()
    {
        base.OnEnable();
        DisableTimer(2.0f);
    }
}
