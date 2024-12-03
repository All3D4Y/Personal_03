using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conclusion : BattleState
{
    public Conclusion(BattleManager manager) : base(manager) {}
    public override void Enter()
    {
        Debug.Log("전투 종료...");
        // 보상 처리, 전투 결과 화면 표시
    }

    public override void Update()
    {
    }

    public override void Exit()
    {
    }
}

