using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Boss Data", menuName = "Scripable Objects/Boss Data", order = 2)]
public class BossData : EnemyDataBase
{
    [SerializeField] protected float ultimateConsumption = 60.0f;
    [Space(20)]
    public Bosses holox = Bosses.Laplus;

    public BossData()
    {
        this.type = EnemyType.Boss;
    }

    public float UltConsuption
    {
        get => ultimateConsumption;
        set => ultimateConsumption = value;
    }
}