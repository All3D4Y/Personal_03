﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BattleState
{
    protected BattleManager manager;

    protected BattleState(BattleManager manager)
    {
        this.manager = manager;
    }

    public virtual void Enter() { }  // 상태 진입 시 실행
    public virtual void Update() { } // 상태 갱신
    public virtual void Exit() { }   // 상태 종료 시 실행
}