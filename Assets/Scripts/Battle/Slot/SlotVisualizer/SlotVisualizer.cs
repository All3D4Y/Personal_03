using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotVisualizer : MonoBehaviour
{
    SlotController slotController;

    Transform[] allyPos;
    Transform[] enemyPos;

    public void Initialize()
    {
        slotController = GameManager.Instance.BattleManager.SlotController;

        allyPos = new Transform[4];
        enemyPos = new Transform[4];

        for (int i = 0; i < 4; i++)
        {
            allyPos[i] = transform.GetChild(0).GetChild(i);
            enemyPos[i] = transform.GetChild(1).GetChild(i);
        }

        GenerateActor();
    }

    void GenerateActor()
    {
        for (int i = 0; i < slotController.AllySlot.Length; i++)
        {
            GetActors(slotController.AllySlot[i].ActorData, allyPos[i].position);
        }
        for (int i = 0; i < slotController.EnemySlot.Length; i++)
        {
            GetActors(slotController.EnemySlot[i].ActorData, enemyPos[i].position);
        }
    }

    GameObject GetActors(Actor actor, Vector2 position)
    {
        GameObject result = null;
        result = Instantiate(actor.gameObject);
        result.transform.position = position;
        return result;
    }
}
