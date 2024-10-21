using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test03_Phase : TestBase
{
    BattleManager battleManager;
    CharacterData[] characterDatas;
    EnemyDataBase[] enemyDatas;


    public Action<IAction> onAction;

    void Start()
    {
        battleManager = GameManager.Instance.BattleManager;

        characterDatas = new CharacterData[5];
        for (int i = 0; i < characterDatas.Length; i++)
        {
            characterDatas[i] = GameManager.Instance.CharacterDataManager[(uint)i] as CharacterData;
        }
        
        enemyDatas = new EnemyDataBase[6];
        for (int i = 0; i < enemyDatas.Length; i++)
        {
            enemyDatas[i] = GameManager.Instance.EnemyDataManager[(uint)i] as EnemyDataBase;
        }
    }

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        battleManager.SlotController.InitialAssign(characterDatas, enemyDatas);
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        battleManager.SetOnTurnSlot(battleManager.TurnCalculator.GetTurnSlot());
    }

    protected override void OnTest3(InputAction.CallbackContext context)
    {
        battleManager.OnMoveRight();
    }

    protected override void OnTest4(InputAction.CallbackContext context)
    {
        battleManager.OnMoveLeft();
    }

    protected override void OnTest5(InputAction.CallbackContext context)
    {
        battleManager.SlotController.TestPrint(0);
        battleManager.SlotController.TestPrint(1);
        battleManager.SlotController.TestPrint(2);
        battleManager.SlotController.TestPrint(3);
    }

    protected override void OnTestRClick(InputAction.CallbackContext context)
    {
        
    }
}
