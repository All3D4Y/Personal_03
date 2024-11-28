using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimFunc : MonoBehaviour
{
    Actor parent;
    void Awake()
    {
        parent =  GetComponentInParent<Actor>();
    }

    public void SetAttackEnd()
    {
        parent.IsAttackEnd = true;
    }

    public void SetHurtEnd()
    {
        parent.IsHurtEnd = true;
    }
}
