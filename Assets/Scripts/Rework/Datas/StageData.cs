using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stage Data", menuName = "Scripable Objects/Stage Data", order = 0)]
public class StageData : ScriptableObject
{
    [Header("스테이지 정보")]
    public uint index = 0;
    //public OldEnemy[] enemyDatas;
    public int enemyLevel = 1;
}
