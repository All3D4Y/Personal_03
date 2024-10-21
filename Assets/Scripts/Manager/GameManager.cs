using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    BattleManager battleManager;
    CharacterDataManager characterDataManager;
    EnemyDataManager enemyDataManager;
    SkillDataManager skillDataManager;
    StageDataManager stageDataManager;

    public BattleManager BattleManager => battleManager;
    public CharacterDataManager CharacterDataManager => characterDataManager;
    public EnemyDataManager EnemyDataManager => enemyDataManager;

    public SkillDataManager SkillDataManager => skillDataManager;

    public StageDataManager StageDataManager => stageDataManager;

    protected override void OnPreInitialize()
    {
        base.OnPreInitialize();
        characterDataManager = GetComponent<CharacterDataManager>();
        enemyDataManager = GetComponent<EnemyDataManager>();
        skillDataManager = GetComponent<SkillDataManager>();
        stageDataManager = GetComponent<StageDataManager>();
    }
    protected override void OnInitialize()
    {
        base.OnInitialize();
        battleManager = FindAnyObjectByType<BattleManager>();
    }
}
