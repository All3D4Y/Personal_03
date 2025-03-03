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
        // 액션 실행 애니메이션이 끝나면 mp--, 받는 애니메이션 재생하는 함수를 실행하는 델리게이트 등록
        manager.OnTurnCharacter.CharacterAnim.onActionAnimEnd -= DoActionAnimEnd;
        manager.OnTurnCharacter.CharacterAnim.onActionAnimEnd += DoActionAnimEnd;
        // 액션 실행 애니메이션 재생
        DoActionAnim();
        // 액션의 효과를 받는 애니메이션 재생 (피격, 버프 이펙트), hp -- UI 갱신
        //manager.ChangeState<StateUpdate>();
    }

    public override void Update()
    {
    }

    public override void Exit()
    {
        // 델리게이트 해제
        manager.OnTurnCharacter.CharacterAnim.onActionAnimEnd -= DoActionAnimEnd;
    }

    void DoActionAnim()
    {
        Skill skill = manager.ActionManager.SelectedAction;
        if (skill != null)
        {
            if (skill is Skill_Attack)
            {
                Skill_Attack temp = skill as Skill_Attack;
                manager.OnTurnCharacter.CharacterAnim.Attack((int)temp.Code);
                if (temp.Code == AttackCode.MagicAttack)
                {
                    bool isRight = manager.OnTurnCharacter.transform.localScale.x == -1 ? true : false;
                    int int_isRight = isRight? 1 : -1;
                    Vector3 position = manager.OnTurnCharacter.transform.position + int_isRight * 5 * Vector3.right;
                    GameManager.Instance.CoroutineManager.OnMagicHitEffect(position, isRight);
                }
            }
            else if (skill is Skill_Buff)
            {
                manager.OnTurnCharacter.CharacterAnim.BuffDebuff();
            }
            else if (skill is Item_Attack)
            {
                manager.OnTurnCharacter.CharacterAnim.Attack(0);
            }
            else if (skill is Item_Heal)
            {
                manager.OnTurnCharacter.CharacterAnim.BuffDebuff();
            }
        }
    }
    void GetActionAnim()
    {
        Skill skill = manager.ActionManager.SelectedAction;
        if (skill != null)
        {
            if (skill is Skill_Attack)
            {
                Skill_Attack attack = skill as Skill_Attack;
                int[] targets = skill.SetTargetIndex(manager.OnTurnCharacter.Index);
                // 받는 애니메이션이 끝나면 hp --, 다음 단계로 넘어가는 함수를 실행하는 델리게이트 등록
                if (manager.OnTurnCharacter.IsPlayer)
                {
                    if (!manager.EnemySlot.GetSlot(targets[targets.Length - 1]).IsEmpty)
                    {
                        manager.EnemySlot.GetSlot(targets[targets.Length - 1]).CharacterData.CharacterAnim.getActionAnimEnd -= ActionGetEnd;
                        manager.EnemySlot.GetSlot(targets[targets.Length - 1]).CharacterData.CharacterAnim.getActionAnimEnd += ActionGetEnd; 
                    }
                    else if (!manager.EnemySlot.GetSlot(targets[targets.Length - 2]).IsEmpty)
                    {
                        manager.EnemySlot.GetSlot(targets[targets.Length - 2]).CharacterData.CharacterAnim.getActionAnimEnd -= ActionGetEnd;
                        manager.EnemySlot.GetSlot(targets[targets.Length - 2]).CharacterData.CharacterAnim.getActionAnimEnd += ActionGetEnd;
                    }
                    else if (!manager.EnemySlot.GetSlot(targets[targets.Length - 3]).IsEmpty)
                    {
                        manager.EnemySlot.GetSlot(targets[targets.Length - 3]).CharacterData.CharacterAnim.getActionAnimEnd -= ActionGetEnd;
                        manager.EnemySlot.GetSlot(targets[targets.Length - 3]).CharacterData.CharacterAnim.getActionAnimEnd += ActionGetEnd;
                    }
                }
                else
                {
                    if (!manager.PlayerSlot.GetSlot(targets[targets.Length - 1]).IsEmpty)
                    {
                        manager.PlayerSlot.GetSlot(targets[targets.Length - 1]).CharacterData.CharacterAnim.getActionAnimEnd -= ActionGetEnd;
                        manager.PlayerSlot.GetSlot(targets[targets.Length - 1]).CharacterData.CharacterAnim.getActionAnimEnd += ActionGetEnd; 
                    }
                    else if (!manager.PlayerSlot.GetSlot(targets[targets.Length - 2]).IsEmpty)
                    {
                        manager.PlayerSlot.GetSlot(targets[targets.Length - 2]).CharacterData.CharacterAnim.getActionAnimEnd -= ActionGetEnd;
                        manager.PlayerSlot.GetSlot(targets[targets.Length - 2]).CharacterData.CharacterAnim.getActionAnimEnd += ActionGetEnd;
                    }
                    else if (!manager.PlayerSlot.GetSlot(targets[targets.Length - 3]).IsEmpty)
                    {
                        manager.PlayerSlot.GetSlot(targets[targets.Length - 3]).CharacterData.CharacterAnim.getActionAnimEnd -= ActionGetEnd;
                        manager.PlayerSlot.GetSlot(targets[targets.Length - 3]).CharacterData.CharacterAnim.getActionAnimEnd += ActionGetEnd;
                    }
                }

                foreach (int target in targets)
                {
                    if (target >= 0)
                    {
                        if (attack.Code == AttackCode.RangedAttack)
                        {
                            if (manager.OnTurnCharacter.IsPlayer)
                            {
                                if (!manager.EnemySlot.GetSlot(target).IsEmpty)
                                    manager.EnemySlot.GetSlot(target).CharacterData.CharacterAnim.HitArrow();
                            }
                            else
                            {
                                if (!manager.PlayerSlot.GetSlot(target).IsEmpty)
                                    manager.PlayerSlot.GetSlot(target).CharacterData.CharacterAnim.HitArrow();
                            }
                        }
                        else if (attack.Code == AttackCode.MeleeAttack)
                        {
                            if (manager.OnTurnCharacter.IsPlayer)
                            {
                                if (!manager.EnemySlot.GetSlot(target).IsEmpty)
                                    manager.EnemySlot.GetSlot(target).CharacterData.CharacterAnim.HitSlash();
                            }
                            else
                            {
                                if (!manager.PlayerSlot.GetSlot(target).IsEmpty)
                                    manager.PlayerSlot.GetSlot(target).CharacterData.CharacterAnim.HitSlash();
                            }
                        }
                        else
                        {
                            if (manager.OnTurnCharacter.IsPlayer)
                            {
                                if (!manager.EnemySlot.GetSlot(target).IsEmpty)
                                    manager.EnemySlot.GetSlot(target).CharacterData.CharacterAnim.Hurt();
                            }
                            else
                            {
                                if (!manager.PlayerSlot.GetSlot(target).IsEmpty)
                                    manager.PlayerSlot.GetSlot(target).CharacterData.CharacterAnim.Hurt();
                            }
                        } 
                    }
                }
            }
            else if (skill is Skill_Buff)
            {
                int[] targets = skill.BuffTargetIndex(manager.OnTurnCharacter.Index);
                // 받는 애니메이션이 끝나면 hp --, 다음 단계로 넘어가는 함수를 실행하는 델리게이트 등록
                if (manager.OnTurnCharacter.IsPlayer)
                {
                    manager.PlayerSlot.GetSlot(targets[targets.Length - 1]).CharacterData.CharacterAnim.getActionAnimEnd -= ActionGetEnd;
                    manager.PlayerSlot.GetSlot(targets[targets.Length - 1]).CharacterData.CharacterAnim.getActionAnimEnd += ActionGetEnd;
                }
                else
                {
                    manager.EnemySlot.GetSlot(targets[targets.Length - 1]).CharacterData.CharacterAnim.getActionAnimEnd -= ActionGetEnd;
                    manager.EnemySlot.GetSlot(targets[targets.Length - 1]).CharacterData.CharacterAnim.getActionAnimEnd += ActionGetEnd;
                }

                foreach (int target in targets)
                {
                    Skill_Buff temp = skill as Skill_Buff;
                    if (!temp.IsDebuff)                                                                 // 버프일 경우에
                    {
                        if (manager.OnTurnCharacter.IsPlayer)                                           // 플레이어가 사용하면
                            manager.PlayerSlot.GetSlot(target).CharacterData.CharacterAnim.GetBuff();   // 플레이어 슬롯에서 버프 애니메이션 재생
                        else                                                                            // 적이 사용하면
                            manager.EnemySlot.GetSlot(target).CharacterData.CharacterAnim.GetBuff();    // 적 슬롯에서 버프 애니메이션 재생
                    }
                    else                                                                                // 디버프일 때
                    {
                        if (manager.OnTurnCharacter.IsPlayer)                                           // 플레이어가 사용하면
                            manager.EnemySlot.GetSlot(target).CharacterData.CharacterAnim.Hurt();       // 적 슬롯에서 디버프 애니메이션 재생
                        else                                                                            // 적이 사용하면
                            manager.PlayerSlot.GetSlot(target).CharacterData.CharacterAnim.Hurt();      // 플레이어 슬롯에서 디버프 애니메이션 재생
                    }
                }
            }
            else if (skill is Item_Attack)
            {
                Item_Attack attack = skill as Item_Attack;
                int[] target;
                if (attack.Range == 0)
                    target = new int[4] { 0, 1, 2, 3 };
                else
                    target = attack.SetTargetIndex(manager.OnTurnCharacter.Index);

                int selectedTargetIndex = -99;

                for (int i = 0; i < target.Length; i++)
                {
                    if (!manager.EnemySlot.GetSlot(i).IsEmpty)
                    {
                        selectedTargetIndex = target[i];
                        break;
                    }
                }

                manager.EnemySlot.GetSlot(selectedTargetIndex).CharacterData.CharacterAnim.getActionAnimEnd -= ActionGetEnd;
                manager.EnemySlot.GetSlot(selectedTargetIndex).CharacterData.CharacterAnim.getActionAnimEnd += ActionGetEnd;
                
                foreach (int targetIndex in target)
                {
                    if (targetIndex >= 0 && !manager.EnemySlot.GetSlot(targetIndex).IsEmpty)
                        manager.EnemySlot.GetSlot(targetIndex).CharacterData.CharacterAnim.Hurt();
                }
            }
            else if (skill is Item_Heal)
            {
                int[] targets = skill.BuffTargetIndex(manager.OnTurnCharacter.Index);

                manager.PlayerSlot.GetSlot(targets[targets.Length - 1]).CharacterData.CharacterAnim.getActionAnimEnd -= ActionGetEnd;
                manager.PlayerSlot.GetSlot(targets[targets.Length - 1]).CharacterData.CharacterAnim.getActionAnimEnd += ActionGetEnd;

                foreach (int targetIndex in targets)
                {
                    manager.PlayerSlot.GetSlot(targetIndex).CharacterData.CharacterAnim.GetBuff();
                }
            }
        }
    }

    void DoActionAnimEnd()
    {
        // 스킬 사용으로 인한 MP --
        manager.OnTurnCharacter.MP -= manager.ActionManager.SelectedAction.MPCost;
        // 피격 애니메이션 재생
        GetActionAnim();
    }

    void ActionGetEnd()
    {
        manager.ActionManager.ActionExecute();
        GameManager.Instance.CoroutineManager.OnChangeState(1.5f, typeof(StateUpdate));
    }
}
