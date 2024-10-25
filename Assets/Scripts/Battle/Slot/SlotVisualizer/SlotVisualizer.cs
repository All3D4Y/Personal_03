using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotVisualizer : MonoBehaviour
{
    SlotController slotController;

    Transform[] allyPos;
    Transform[] enemyPos;

    GameObject[] allies;
    GameObject[] enemies;

    GameObject onTurn = null;
    public void Initialize()
    {
        slotController = GameManager.Instance.BattleManager.SlotController;

        GameManager.Instance.BattleManager.BattleInput.onScroll += OnMoveSlot;

        allyPos = new Transform[4];
        enemyPos = new Transform[4];

        for (int i = 0; i < 4; i++)
        {
            allyPos[i] = transform.GetChild(0).GetChild(i);
            enemyPos[i] = transform.GetChild(1).GetChild(i);
        }

        allies = new GameObject[slotController.AllySlot.Length];
        enemies = new GameObject[slotController.EnemySlot.Length];

        for (int i = 0; i < slotController.AllySlot.Length; i++)
        {
            allies[i] = GetActor(slotController.AllySlot[i].ActorData);
        }

        for (int i = 0; i < slotController.EnemySlot.Length; i++)
        {
            enemies[i] = GetActor(slotController.EnemySlot[i].ActorData);
        }

        for(int i = 0; i< allies.Length; i++)
        {
            GetIn(allies[i], allyPos[i]);
        }

        for (int i = 0; i < enemies.Length; i++)
        {
            GetIn(enemies[i], enemyPos[i]);
        }

        GameManager.Instance.BattleManager.onTurnSet += (side, index) =>
        {
            if (side == ActorSide.Ally)
            {
                onTurn = allyPos[index].GetChild(0).gameObject;
            }
            else
            {
                onTurn = enemyPos[index].GetChild(0).gameObject;
            }
        };
    }

    void GetIn(GameObject actor, Transform pos)
    {
        actor.transform.SetParent(pos);
        actor.transform.localPosition = Vector3.zero;
    }

    GameObject GetActor(Actor actor)
    {
        return Instantiate(actor.gameObject);
    }

    void OnMoveSlot(int input)
    {
        int temp = onTurn.transform.parent.GetSiblingIndex();
        if ((temp > 0 && input == -1) || (temp < 3 && input == 1))
        {
            if (onTurn.GetComponent<Actor>() is Ally)
            {
                allyPos[temp + input].GetChild(0).SetParent(onTurn.transform.parent);
                onTurn.transform.parent.GetChild(1).localPosition = Vector3.zero;
                onTurn.transform.SetParent(allyPos[temp + input]);
                allyPos[temp + input].GetChild(0).localPosition = Vector3.zero;
            }
            else
            {
                enemyPos[temp + input].GetChild(0).SetParent(onTurn.transform.parent);
                onTurn.transform.parent.GetChild(1).localPosition = Vector3.zero;
                onTurn.transform.SetParent(allyPos[temp + input]);
                enemyPos[temp + input].GetChild(0).localPosition = Vector3.zero;
            }
        }
    }
}
