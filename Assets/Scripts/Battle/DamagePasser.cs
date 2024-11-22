using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePasser
{
    float damage;

    public float Damage => damage;

    public DamagePasser(float damage)
    {
        this.damage = damage;
    }
}
