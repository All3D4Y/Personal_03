using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnim : MonoBehaviour
{
    Animator animator;

    public event Action onActionAnimEnd;
    public event Action getActionAnimEnd;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void MeleeAttack()
    {
        animator.SetFloat("AttackState", 0.6666f);
        animator.SetTrigger("Attack");
    }

    void RangedAttack()
    {
        animator.SetFloat("AttackState", 0.0f);
        animator.SetTrigger("Attack");
    }

    void MagicAttack()
    {
        animator.SetFloat("AttackState", 1.0f);
        animator.SetTrigger("Attack");
    }

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

    public void BuffDebuff()
    {
        animator.SetFloat("AttackState", 1.0f);
        animator.SetTrigger("Attack");
    }
    public void GetBuff()
    {
        animator.SetFloat("AffectState", 1.0f);
        animator.SetTrigger("Affect");
    }

    public void Hurt()
    {
        animator.SetFloat("AffectState", 0.0f);
        animator.SetTrigger("Affect");
    }

    public void Death()
    {
        animator.SetTrigger("Die");
    }

    public void OnActionEnd()
    {
        Debug.Log("액션 실행 애니메이션 종료");
        onActionAnimEnd?.Invoke();
    }

    public void GetActionEnd()
    {
        Debug.Log("액션 효과 적용 애니메이션 종료");
        getActionAnimEnd?.Invoke();
    }

    public void ShootArrow()
    {
        bool temp = transform.parent.localScale.x == -1 ? true : false;
        Factory.Instance.GetArrow(transform.position, temp);
    }

    public void HitArrow()
    {
        bool temp = transform.parent.localScale.x == -1? true : false;
        Factory.Instance.GetArrowHitEffect(transform.position, temp);
        Hurt();
    }

    public void HitSlash()
    {
        bool temp = transform.parent.localScale.x == -1? true : false;
        Factory.Instance.GetSlashHitEffect(transform.position, temp);
        Hurt();
    }

    public void HitMagic()
    {
        bool temp = transform.parent.localScale.x == -1? true : false;
        Factory.Instance.GetMagicHitEffect(transform.position, temp);
    }

#if UNITY_EDITOR
    public void TestRangedAttack()
    {
        RangedAttack();
    }
#endif
}
