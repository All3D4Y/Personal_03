using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnEnd : BattleState
{
    public TurnEnd(BattleManager manager) : base(manager) {}

    public override void Enter()
    {
        Debug.Log("턴 종료 단계...");
        manager.TurnOrder.GetNextCharacter();
        if (manager.TurnOrder.IsBattleOver())
            manager.ChangeState<Conclusion>();
        else
            manager.ChangeState<SelectAction>();
    }

    public override void Update()
    {
    }

    public override void Exit()
    {
    }
}
