using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Spoils { public Item item; public int count; }

[CreateAssetMenu(fileName = "New Stage Data", menuName = "Scripable Objects/Stage Data", order = 0)]
public class StageData : ScriptableObject
{
    [Header("스테이지 정보")]
    public uint index = 0;
    public int[] enemyCodes;
    public int enemyLevel = 1;

    [Header("보상")]
    public float exp = 0;
    public Spoils[] spoils;

    [Header("BackGround")]
    public Sprite bg;
}
