﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    SlotTransformContainer slotTransform;
    BattleManager battleManager;
    BattleUIManager battleUIManager;
    CoroutineManager coroutineManager;
    BackGroundManager backGroundManager;


    public SlotTransformContainer SlotTransform => slotTransform;
    public BattleManager BattleManager => battleManager;
    public BattleUIManager BattleUIManager => battleUIManager;
    public CoroutineManager CoroutineManager => coroutineManager;
    public BackGroundManager BackGroundManager => backGroundManager;

    public CurrentPosition? CurrentPosition { get; set; }

    protected override void OnInitialize()
    {
        slotTransform = FindAnyObjectByType<SlotTransformContainer>();
        battleManager = FindAnyObjectByType<BattleManager>();
        battleUIManager = FindAnyObjectByType<BattleUIManager>();
        coroutineManager = GetComponent<CoroutineManager>();
        backGroundManager = FindAnyObjectByType<BackGroundManager>();
    }

    /// <summary>
    /// 게임 종료 함수
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
