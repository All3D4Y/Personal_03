using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test02_Battle : TestBase
{
    SlotController controller;

    CharacterData[] characterDatas;
    EnemyDataBase[] enemyDatas;

    void Start()
    {
        controller = new SlotController();

        characterDatas = new CharacterData[5];
        for (int i = 0; i < characterDatas.Length; i++)
        {
            characterDatas[i] = GameManager.Instance.CharacterDataManager[(uint)i]as CharacterData;
        }

        enemyDatas = new EnemyDataBase[6];
        for (int i = 0; i < enemyDatas.Length; i++)
        {
            enemyDatas[i] = GameManager.Instance.EnemyDataManager[(uint)i] as EnemyDataBase;
        }


    }

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        controller.TestPrint();
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        controller.AssignSlot(characterDatas[0], 2);
        controller.AssignSlot(characterDatas[1], 3);
        controller.AssignSlot(enemyDatas[1], 2);
        controller.AssignSlot(characterDatas[3], 2, true);
    }

    protected override void OnTest3(InputAction.CallbackContext context)
    {
        controller.InitialAssign(characterDatas, enemyDatas);
    }

    protected override void OnTest4(InputAction.CallbackContext context)
    {
        controller.ClearAllSlot();
    }

    protected override void OnTest5(InputAction.CallbackContext context)
    {
        controller.SortAllSlot();
    }
}   