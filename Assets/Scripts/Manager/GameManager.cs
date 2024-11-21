using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    BattleManager battleManager;
    SlotController slotController;
    StageDataManager stageDataManager;
    SlotVisualizer slotVisualizer;
    BattleUI battleUI;
    SkillGroupUI skillUI;
    SkillGuideLine guideLine;

    public BattleManager BattleManager => battleManager;

    public SlotController SlotController => battleManager.SlotController;

    public StageDataManager StageDataManager => stageDataManager;

    public SlotVisualizer SlotVisualizer => slotVisualizer;

    public BattleUI BattleUI => battleUI;

    public SkillGroupUI SkillUI => skillUI;

    public SkillGuideLine GuideLine => guideLine;

    protected override void OnPreInitialize()
    {
        base.OnPreInitialize();
        stageDataManager = GetComponent<StageDataManager>();
    }
    protected override void OnInitialize()
    {
        base.OnInitialize();
        battleManager = FindAnyObjectByType<BattleManager>();
        slotVisualizer = FindAnyObjectByType<SlotVisualizer>();
        battleUI = FindAnyObjectByType<BattleUI>();
        skillUI = FindAnyObjectByType<SkillGroupUI>();
        guideLine = FindAnyObjectByType<SkillGuideLine>();
    }
}
