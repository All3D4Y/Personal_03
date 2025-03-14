using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effects : RecycleObject
{
    /// <summary>
    /// 활성화 시 2초 후 비활성화
    /// </summary>
    protected override void OnEnable()
    {
        base.OnEnable();
        DisableTimer(2.0f);
    }
}
