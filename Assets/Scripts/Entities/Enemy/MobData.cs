using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Mob Data", menuName = "Scripable Objects/Mob Data", order = 1)]
public class MobData : EnemyDataBase
{
    public MobData()
    {
        this.type = EnemyType.Mob;
    }
}