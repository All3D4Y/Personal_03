using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageNumberUI : RecycleObject
{
    Animator animator;

    SpriteRenderer spriteRenderer;

    Color critical = new Color(1, 0.7f, 0.7f);

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

    public void SetNumber(int number)
    {
        animator.SetFloat("Number", number * 0.1f);
    }

    public void Critical()
    {
        spriteRenderer.color = critical;
    }
}
