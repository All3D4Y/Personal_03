using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager : Singleton<PlayerDataManager>
{
    public int playerLevel = 1;
    public int[] players;

    float exp;
    public Action onLevelUp;
    public float Exp
    {
        get => exp;
        set
        {
            exp = value;
            if (exp >= LevelUpThreshold)
            {
                playerLevel++;
                exp = 0;
                onLevelUp?.Invoke();
            }
        }
    }
    public float LevelUpThreshold => 50 * Mathf.Pow(playerLevel, 2) + 50;
    public float LevelUpRatio => exp / LevelUpThreshold;

    void Reset()
    {
        players = new int[8];
    }
}
