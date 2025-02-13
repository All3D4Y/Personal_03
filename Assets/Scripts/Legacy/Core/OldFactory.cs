using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldFactory : Singleton<OldFactory>
{
    ActorPool ally_00;
    ActorPool ally_01;
    ActorPool ally_02;
    ActorPool ally_03;

    ActorPool enemy_00;
    ActorPool enemy_01;

    EffectPool slashEffect;
    EffectPool magicEffect;
    EffectPool arrowEffect;

    ArrowPool arrow;

    protected override void OnInitialize()
    {
    }

    public Actor GetActor(int index)
    {
        ActorPool temp = null;

        switch (index)
        {
            case 0:
                temp = ally_00;
                break;
            case 1:
                temp = ally_01;
                break;
            case 2:
                temp = ally_02;
                break;
            case 3:
                temp = ally_03;
                break;
            case 4:
                temp = enemy_00;
                break;
            case 5:
                temp = enemy_01;
                break;
        }
        return temp.GetObject();
    }

    public Effects GetEffect(int index)
    {
        EffectPool temp = null;

        switch (index)
        {
            case 0:
                temp = slashEffect;
                break;
            case 1:
                temp = magicEffect;
                break;
            case 2:
                temp = arrowEffect;
                break;
        }
        return temp.GetObject();
    }
}
