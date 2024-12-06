using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Execution : BattleState
{
    public Execution(BattleManager manager) : base(manager) {}

    public override void Enter()
    {
        Debug.Log("행동 실행 단계...");
        // 선택한 액션을 실행하는 애니메이션 재생 (공격, 버프 사용)
        // 액션의 효과를 받는 애니메이션 재생 (피격, 버프 이펙트)
        
        manager.ChangeState<StateUpdate>();
    }

    public override void Update()
    {
    }

    public override void Exit()
    {
        // 데미지 계산, 버프 적용
    }
}
