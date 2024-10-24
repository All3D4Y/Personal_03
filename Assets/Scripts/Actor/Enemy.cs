using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Actor
{
    [Space(20)]
    [SerializeField] protected EnemyType enemyType;
    void Reset()
    {
        side = ActorSide.Enemy;
    }

    public override void Animation()
    {
    }
}
