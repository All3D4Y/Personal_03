using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldEnemy : Actor
{
    [Space(20)]
    [SerializeField] protected EnemyType enemyType;
    void Reset()
    {
        side = ActorSide.Enemy;
    }

    public override void AttackAnimation(int num)
    {
        switch (num)
        {
            case 0:
                spum.PlayAnimation(4);
                break;
            case 1:
                spum.PlayAnimation(5);
                break;
        }
    }
}
