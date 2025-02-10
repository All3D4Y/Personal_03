using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAction : MonoBehaviour
{
    Character onTurnEnemy;

    public bool CanValidAct()
    {
        bool result = false;
        foreach (Skill skill in onTurnEnemy.skillDatas)
        {
            result |= skill.IsValid();
        }
        return result;
    }

    public Slot FindValidTarget()
    {
        Slot result = null;

        return result;
    }

    public void OnMoveValidSlot()
    {

    }
}
