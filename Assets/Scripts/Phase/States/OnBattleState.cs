using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnBattleState : IPhase
{
    PhaseManager phaseManager;

    public OnBattleState(PhaseManager phaseManager)
    {
        this.phaseManager = phaseManager;
    }

    public void Enter()
    {
        // 전투단계 진입 시 실행할 코드
        // 선택한 행동 전달
    }
    public void Exit()
    {
        // 전투단계에서 종료 시 실행할 코드
        // 대미지 적용 및 한 쪽 진영 전멸 여부에 따라 BattleEnd or BattlePrep 어느 상태로 이동할지 결정
    }
    public void Execute()
    {
        // 전투단계 진행 중 실행할 코드
        // 애니메이션 재생, 대미지 계산
    }
}