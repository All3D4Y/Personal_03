using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally : Actor
{
    void Reset()
    {
        side = ActorSide.Ally;
    }

    public override void Animation()
    {
    }
}
