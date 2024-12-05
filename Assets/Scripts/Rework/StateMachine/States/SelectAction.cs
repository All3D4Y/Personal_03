using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectAction : BattleState
{
    public SelectAction(BattleManager manager) : base(manager) {}

    public override void Enter()
    {
        Debug.Log("액션 선택 중...");
        // UI 활성화
        
    }

    public override void Update()
    {
    }

    public override void Exit()
    {
        // UI 비활성화
        // 선택한 액션을 실행하는 애니메이션 재생 (공격, 버프 사용)
        // 액션의 효과를 받는 애니메이션 재생 (피격, 버프 이펙트)
    }
}
