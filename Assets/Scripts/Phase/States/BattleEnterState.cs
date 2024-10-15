using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEnterState : IPhase
{
    PhaseManager phaseManager;

    public BattleEnterState(PhaseManager phaseManager)
    {
        this.phaseManager = phaseManager;
    }

    public void Enter()
    {
        // 전투 진입단계 진입 시 실행할 코드
        // 로딩
    }
    public void Exit()
    {
        // 전투 진입단계에서 종료 시 실행할 코드
    }
    public void Execute()
    {
        // 전투 진입단계 진행 중 실행할 코드
        // 전투 진입 애니메이션 재생 및 UI활성화
    }
}
