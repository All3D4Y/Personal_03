using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageNumberUI : RecycleObject
{
    Animator animator;

    SpriteRenderer spriteRenderer;

    Color critical = new Color(1, 0.7f, 0.7f);      // 크리티컬 발생 시 변경할 색상

    void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        DisableTimer(1.5f);
    }

    protected override void OnReset()
    {
        spriteRenderer.color = Color.white;
    }

    /// <summary>
    /// 이 UI가 표시할 숫자를 정하는 함수
    /// </summary>
    /// <param name="number"></param>
    public void SetNumber(int number)
    {
        animator.SetFloat("Number", number * 0.1f);
    }

    /// <summary>
    /// 크리티컬 발생 시 색상을 변경하는 함수
    /// </summary>
    public void Critical()
    {
        spriteRenderer.color = critical;
    }
}
