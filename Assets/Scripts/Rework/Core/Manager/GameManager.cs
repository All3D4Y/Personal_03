using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    SlotTransformContainer slotTransform;
    BattleManager battleManager;
    BattleUIManager battleUIManager;
    CoroutineManager coroutineManager;

    public SlotTransformContainer SlotTransform => slotTransform;
    public BattleManager BattleManager => battleManager;
    public BattleUIManager BattleUIManager => battleUIManager;
    public CoroutineManager CoroutineManager => coroutineManager;

    protected override void OnInitialize()
    {
        slotTransform = FindAnyObjectByType<SlotTransformContainer>();
        battleManager = FindAnyObjectByType<BattleManager>();
        battleUIManager = FindAnyObjectByType<BattleUIManager>();
        coroutineManager = GetComponent<CoroutineManager>();
    }
}
