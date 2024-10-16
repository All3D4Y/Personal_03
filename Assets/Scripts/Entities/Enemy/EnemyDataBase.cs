using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Mob = 0,
    Boss
}

public class EnemyDataBase : EntityData
{
    [Tooltip("인스펙터에서 수정하지 마시오.")]
    public EnemyType type;


    public override void Animation()
    {
        // animation
    }

    public override void Skill()
    {
        // skill?
    }
}
