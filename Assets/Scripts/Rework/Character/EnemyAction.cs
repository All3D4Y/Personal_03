using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAction
{
    BattleManager battleManager;
    Character onTurnEnemy;

    public void Initialize()
    {
        battleManager = GameManager.Instance.BattleManager;
        onTurnEnemy = battleManager.OnTurnCharacter;
    }

    public bool CanValidAct()
    {
        return onTurnEnemy.skillDatas[0].IsValid();
    }

    public int ValidSlotIndex()
    {
        int result = -999;
        if (!CanValidAct())
        {
            int index = 3;
            while (index >= 0)
            {
                if (onTurnEnemy.skillDatas[0].IsValid(index))
                {
                    result = index;
                    break;
                }
                index--;
            }
            if (index != result)
            {
                result = index;
            }
        }

        return result;
    }

    public void OnMoveValidSlot()
    {
        int temp = ValidSlotIndex();
        if (temp > -1)
        {
            battleManager.EnemySlot.SwapCharacter(onTurnEnemy.Index, temp);
        }
        else if (temp == -1)
        {
            // 아무것도 못하는 상태, 턴 넘기기
            battleManager.ChangeState<TurnEnd>();
        }
        battleManager.ActionManager.SetAction(onTurnEnemy.skillDatas[0]);
        battleManager.ChangeState<Execution>();
    }
}
