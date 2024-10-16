using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDataManager : EntityDataManager
{
    EnemyDataBase[] enemies;
    public override EntityData this[uint index] 
    {
        get => enemies[index];
        set => enemies[index] = value as EnemyDataBase;
    }
}
