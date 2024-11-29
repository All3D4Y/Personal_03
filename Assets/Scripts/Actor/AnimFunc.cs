using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimFunc : MonoBehaviour
{
    Actor parent;

    public Action onAttackAnimEnd;
    public Action onHurtAnimEnd;

    void Awake()
    {
        parent =  GetComponentInParent<Actor>();
    }

    public void AttackEnd()
    {
        onAttackAnimEnd?.Invoke();
    }

    public void HurtEnd()
    {
        onHurtAnimEnd?.Invoke();
    }
}
