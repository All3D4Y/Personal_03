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
        // 풀 초기화
        Transform child;

        child = transform.GetChild(0);
        ally_00 = child.GetComponent<ActorPool>();
        if (ally_00 != null)
            ally_00.Initialize();

        child = transform.GetChild(1);
        ally_01 = child.GetComponent<ActorPool>();
        if (ally_01 != null)
            ally_01.Initialize();

        child = transform.GetChild(2);
        ally_02 = child.GetComponent<ActorPool>();
        if (ally_02 != null)
            ally_02.Initialize();

        child = transform.GetChild(3);
        ally_03 = child.GetComponent<ActorPool>();
        if (ally_03 != null)
            ally_03.Initialize();

        child = transform.GetChild(4);
        enemy_00 = child.GetComponent<ActorPool>();
        if (enemy_00 != null)
            enemy_00.Initialize();

        child = transform.GetChild(5);
        enemy_01 = child.GetComponent<ActorPool>();
        if (enemy_01 != null)
            enemy_01.Initialize();

        child = transform.GetChild(6);
        slashEffect = child.GetComponent<EffectPool>();
        if (slashEffect != null)
            slashEffect.Initialize();

        child = transform.GetChild(7);
        magicEffect = child.GetComponent<EffectPool>();
        if (magicEffect != null)
            magicEffect.Initialize();

        child = transform.GetChild(8);
        arrowEffect = child.GetComponent<EffectPool>();
        if (arrowEffect != null)
            arrowEffect.Initialize();

        child = transform.GetChild(9);
        arrow = child.GetComponent<ArrowPool>();
        if (arrow != null)
            arrow.Initialize();
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

    public Arrow GetArrow(Vector2 pos, bool isRight = false)
    {
        Arrow temp = isRight ? arrow.GetObject(pos, new Vector3(0, 0, 180)) : arrow.GetObject(pos);

        return temp;
    }
}
