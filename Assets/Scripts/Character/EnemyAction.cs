using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAction
{
    BattleManager battleManager;
    Character onTurnEnemy;          // 현재 차례인 적 캐릭터

    /// <summary>
    /// 초기화 함수
    /// </summary>
    public void Initialize()
    {
        battleManager = GameManager.Instance.BattleManager;
        onTurnEnemy = battleManager.OnTurnCharacter;
    }

    /// <summary>
    /// 현재 스킬을 사용할 수 있는지 확인하는 함수
    /// </summary>
    /// <returns>현재 스킬 사용이 가능하면 true 반환</returns>
    public bool CanValidAct()
    {
        return onTurnEnemy.skillDatas[0].IsValid();
    }

    /// <summary>
    /// 스킬 사용이 가능한 슬롯의 인덱스를 반환하는 함수
    /// </summary>
    /// <returns>스킬 사용이 가능한 슬롯의 인덱스</returns>
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

    /// <summary>
    /// 스킬을 사용할 수 있는 슬롯으로 이동하는 함수
    /// </summary>
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
