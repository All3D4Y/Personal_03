using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally : Actor
{
    void Reset()
    {
        side = ActorSide.Ally;
    }

    public override void AttackAnimation(int num)
    {
        switch (num)
        {
            case 0:
                spum.PlayAnimation(4);
                break;
            case 1:
                spum.PlayAnimation(4);
                break;
            case 2:
                spum.PlayAnimation(9);
                break;
            case 3:
                spum.PlayAnimation(6);
                break;
        }
    }
}
