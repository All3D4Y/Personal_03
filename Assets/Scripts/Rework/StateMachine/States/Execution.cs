using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Execution : BattleState
{
    public Execution(BattleManager manager) : base(manager) {}

    public override void Enter()
    {
        Debug.Log("행동 실행 단계...");
        // 선택한 액션을 실행하는 애니메이션 재생 (공격, 버프 사용), mp -- UI 갱신
        manager.OnTurnCharacter.CharacterAnim.onActionAnimEnd += ActionExecutionEnd; // 액션 실행 애니메이션이 끝나면 mp--, 받는 애니메이션 재생
        manager.OnTurnCharacter.CharacterAnim.getActionAnimEnd += ActionGetEnd;      // 받는 애니메이션이 끝나면 hp --, 다음 단계로
        DoActionAnim();
        // 액션의 효과를 받는 애니메이션 재생 (피격, 버프 이펙트), hp -- UI 갱신
        //manager.ChangeState<StateUpdate>();
    }

    public override void Update()
    {
    }

    public override void Exit()
    {
        // 데미지 계산, 버프 적용
    }

    void DoActionAnim()
    {
        ItemSkill skill = manager.ActionManager.SelectedAction;
        if (skill != null)
        {
            if (skill is Skill_Attack)
            {
                Skill_Attack temp = skill as Skill_Attack;
                manager.OnTurnCharacter.CharacterAnim.Attack((int)temp.Code);
            }
            else if (skill is Skill_Buff)
            {
                manager.OnTurnCharacter.CharacterAnim.BuffDebuff();
            }
        }
    }
    void GetActionAnim()
    {
        Debug.Log("피격 or 버프 애니메이션 재생, 적용");
        manager.ActionManager.ActionExecute();
    }

    void ActionExecutionEnd()
    {
        manager.OnTurnCharacter.MP -= manager.ActionManager.SelectedAction.MPCost;
        GetActionAnim();
    }

    void ActionGetEnd()
    {
        manager.ChangeState<StateUpdate>();
    }
}
