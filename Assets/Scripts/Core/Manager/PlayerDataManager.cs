using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager : Singleton<PlayerDataManager>
{
    public int playerLevel = 1;
    public int[] players;

    float exp;

    /// <summary>
    /// 레벨업을 알리는 델리게이트
    /// </summary>
    public Action onLevelUp;

    /// <summary>
    /// 경험치
    /// </summary>
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

    /// <summary>
    /// 현재 레벨에 따른 레벨업에 필요한 경험치의 양
    /// </summary>
    public float LevelUpThreshold => 50 * Mathf.Pow(playerLevel, 2) + 50;

    /// <summary>
    /// UI용 레벨업까지 필요한 현재 경험치 비율
    /// </summary>
    public float LevelUpRatio => exp / LevelUpThreshold;

    void Reset()
    {
        players = new int[8];
    }
}
