using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePrepState : IPhase
{
    PhaseManager phaseManager;

    public BattlePrepState(PhaseManager phaseManager)
    {
        this.phaseManager = phaseManager;
    }

    public void Enter()
    {
        // 전투 준비단계 진입 시 실행할 코드
        // 턴 계산
    }
    public void Exit() 
    {
        // 전투 준비단계에서 종료 시 실행할 코드
    }
    public void Execute() 
    {
        // 전투 준비단계 진행 중 실행할 코드
    }
}
