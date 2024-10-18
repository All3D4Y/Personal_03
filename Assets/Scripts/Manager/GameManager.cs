using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    CharacterDataManager characterDataManager;
    EnemyDataManager enemyDataManager;

    protected override void OnPreInitialize()
    {
        base.OnPreInitialize();
        characterDataManager = GetComponent<CharacterDataManager>();
        enemyDataManager = GetComponent<EnemyDataManager>();
    }
    protected override void OnInitialize()
    {
    }
}
