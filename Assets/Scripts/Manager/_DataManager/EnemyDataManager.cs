using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDataManager : ActorDataManager
{
    public Enemy[] enemies;

    public override Actor this[uint index] 
    { 
        get => enemies[index]; 
        set => enemies[index] = value as Enemy; 
    }
}
