using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldGameManager : Singleton<OldGameManager>
{
    OldBattleManager battleManager;
    SlotController slotController;
    OldStageDataManager stageDataManager;
    SlotVisualizer slotVisualizer;
    BattleUI battleUI;
    SkillGroupUI skillUI;
    SkillGuideLine guideLine;

    public OldBattleManager BattleManager => battleManager;

    public SlotController SlotController => battleManager.SlotController;

    public OldStageDataManager StageDataManager => stageDataManager;

    public SlotVisualizer SlotVisualizer => slotVisualizer;

    public BattleUI BattleUI => battleUI;

    public SkillGuideLine GuideLine => guideLine;

    protected override void OnPreInitialize()
    {
        base.OnPreInitialize();
        stageDataManager = GetComponent<OldStageDataManager>();
    }
    protected override void OnInitialize()
    {
        base.OnInitialize();
        battleManager = FindAnyObjectByType<OldBattleManager>();
        slotVisualizer = FindAnyObjectByType<SlotVisualizer>();
        battleUI = FindAnyObjectByType<BattleUI>();
        guideLine = FindAnyObjectByType<SkillGuideLine>();
    }
}
