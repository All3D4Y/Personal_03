using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEndState : IPhase
{
    PhaseManager phaseManager;

    public BattleEndState(PhaseManager phaseManager)
    {
        this.phaseManager = phaseManager;
    }

    public void Enter()
    {
        // 전투종료 단계 진입 시 실행할 코드
        // 전투 결산 UI 활성화 및 데이터 정리
    }
    public void Exit()
    {
        // 전투종료 단계에서 종료 시 실행할 코드
        // UI 비활성화
        // 미니맵 씬으로 이동
    }
    public void Execute()
    {
        // 전투종료 단계 진행 중 실행할 코드
        // 결산 화면 애니메이션 재생
    }
}