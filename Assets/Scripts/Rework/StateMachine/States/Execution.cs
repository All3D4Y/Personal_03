using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Execution : BattleState
{
    public Execution(BattleManager manager) : base(manager) {}

    public override void Enter()
    {
        Debug.Log("행동 실행 단계...");
        // 데미지 계산, 버프 적용
        manager.ChangeState<StateUpdate>();
    }

    public override void Update()
    {
    }

    public override void Exit()
    {
    }
}
