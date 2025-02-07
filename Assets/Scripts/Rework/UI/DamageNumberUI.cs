using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageNumberUI : RecycleObject
{
    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        DisableTimer(2f);
    }

    public void SetNumber(int number)
    {
        animator.SetFloat("Number", number * 0.1f);
    }
}
