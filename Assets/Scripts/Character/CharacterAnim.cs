using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnim : MonoBehaviour
{
    Animator animator;

    /// <summary>
    /// 행동 애니메이션이 끝났음을 알리는 델리게이트
    /// </summary>
    public event Action onActionAnimEnd;

    /// <summary>
    /// 반응 애니메이션이 끝났음을 알리는 델리게이트
    /// </summary>
    public event Action getActionAnimEnd;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// 근접 공격 애니메이션
    /// </summary>
    void MeleeAttack()
    {
        animator.SetFloat("AttackState", 0.6666f);
        animator.SetTrigger("Attack");
    }

    /// <summary>
    /// 원거리 공격 애니메이션
    /// </summary>
    void RangedAttack()
    {
        animator.SetFloat("AttackState", 0.0f);
        animator.SetTrigger("Attack");
    }

    /// <summary>
    /// 마법 공격 애니메이션
    /// </summary>
    void MagicAttack()
    {
        animator.SetFloat("AttackState", 1.0f);
        animator.SetTrigger("Attack");
    }

    /// <summary>
    /// 공격 애니메이션
    /// </summary>
    /// <param name="index">0 = 근접, 1 = 원거리, 2 = 마법</param>
    public void Attack(int index)
    {
        switch (index)
        {
            case 0:
                MeleeAttack();
                break;
            case 1:
                RangedAttack();
                break;
            case 2:
                MagicAttack();
                break;
        }
    }

    /// <summary>
    /// 버프나 디버프를 거는 애니메이션
    /// </summary>
    public void BuffDebuff()
    {
        animator.SetFloat("AttackState", 1.0f);
        animator.SetTrigger("Attack");
    }

    /// <summary>
    /// 버프를 받는 애니메이션
    /// </summary>
    public void GetBuff()
    {
        animator.SetFloat("AffectState", 1.0f);
        animator.SetTrigger("Affect");
    }

    /// <summary>
    /// 피격 애니메이션
    /// </summary>
    public void Hurt()
    {
        animator.SetFloat("AffectState", 0.0f);
        animator.SetTrigger("Affect");
    }

    /// <summary>
    /// 죽는 애니메이션
    /// </summary>
    public void Death()
    {
        animator.SetTrigger("Die");
    }

    /// <summary>
    /// 행동 애니메이션 종료 시 호출되는 함수
    /// </summary>
    public void OnActionEnd()
    {
#if UNITY_EDITOR
        Debug.Log("액션 실행 애니메이션 종료");
#endif
        onActionAnimEnd?.Invoke();
    }

    /// <summary>
    /// 반응 애니메이션 종료 시 호출되는 함수
    /// </summary>
    public void GetActionEnd()
    {
#if UNITY_EDITOR
        Debug.Log("액션 효과 적용 애니메이션 종료"); 
#endif
        getActionAnimEnd?.Invoke();
    }

    /// <summary>
    /// 활 쏘는 애니메이션, 화살 발사 포함
    /// </summary>
    public void ShootArrow()
    {
        bool temp = transform.parent.localScale.x == -1 ? true : false;
        Factory.Instance.GetArrow(transform.position, temp);
    }

    /// <summary>
    /// 화살에 맞는 애니메이션, 피격 이펙트 포함
    /// </summary>
    public void HitArrow()
    {
        bool temp = transform.parent.localScale.x == -1? true : false;
        Factory.Instance.GetArrowHitEffect(transform.position, temp);
        Hurt();
    }

    /// <summary>
    /// 베기 공격에 맞는 애니메이션, 피격 이펙트 포함
    /// </summary>
    public void HitSlash()
    {
        bool temp = transform.parent.localScale.x == -1? true : false;
        Factory.Instance.GetSlashHitEffect(transform.position, temp);
        Hurt();
    }

    /// <summary>
    /// 마법 공격에 맞는 애니메이션, 마법 이펙트 포함
    /// </summary>
    public void HitMagic()
    {
        bool temp = transform.parent.localScale.x == -1? true : false;
        Factory.Instance.GetMagicHitEffect(transform.position, temp);
    }

#if UNITY_EDITOR
    /// <summary>
    /// 테스트용
    /// </summary>
    public void TestRangedAttack()
    {
        RangedAttack();
    }
#endif
}
