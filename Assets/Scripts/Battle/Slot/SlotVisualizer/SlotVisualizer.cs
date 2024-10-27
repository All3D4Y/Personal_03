using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotVisualizer : MonoBehaviour
{
    SlotController slotController;

    Transform[] allyPos;
    Transform[] allyStandbyPos;
    Transform[] enemyPos;
    Transform[] enemyStandbyPos;

    GameObject[] allies;
    GameObject[] enemies;

    GameObject onTurn = null;


    public void Initialize()
    {
        if (allyPos == null)
        {
            slotController = GameManager.Instance.SlotController;

            allyPos = new Transform[4];
            allyStandbyPos = new Transform[4];
            enemyPos = new Transform[4];
            enemyStandbyPos = new Transform[4];

            for (int i = 0; i < 4; i++)
            {
                allyPos[i] = transform.GetChild(0).GetChild(i);
                enemyPos[i] = transform.GetChild(1).GetChild(i);
                allyStandbyPos[i] = transform.GetChild(2).GetChild(i);
                enemyStandbyPos[i] = transform.GetChild(3).GetChild(i);
            }

            // 턴 정해지면 전달하기
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
            GameManager.Instance.BattleManager.BattleInput.onScroll += OnMoveSlot;

            SetActiveStageData();
        }
    }
    public void SetActiveStageData()
    {
        StageDataManager sdm = GameManager.Instance.StageDataManager;

        int[] index_Ally = sdm.GetStageAllyIndex();
        int[] index_Enemy = sdm.GetStageEnemyIndex(sdm.CurrentStageIndex);

        allies = new GameObject[index_Ally.Length];
        enemies = new GameObject[index_Enemy.Length];

        for (int i = 0; i < allies.Length; i++)
        {
            Actor temp = Factory.Instance.GetActor(index_Ally[i]);  // 팩토리에서 가져와서
            if (i < 4)                                              // 0~3 까지는
            {
                GetIn(temp.gameObject, allyPos[i]);                 // 슬롯 위치에 보여지게 넣고
                GameManager.Instance.SlotController.AllySlot[i].AssignData(temp);   // 데이터에 넣기
            }
            else
            {
                GetIn(temp.gameObject, allyStandbyPos[i - 4]);          // 넘어간 것들은 대기석에 넣기
                GameManager.Instance.SlotController.AllyStandbySlot[i - 4].AssignData(temp);
            }
        }

        for (int i = 0; i < enemies.Length; i++)
        {
            Actor temp = Factory.Instance.GetActor(index_Enemy[i]); // 팩토리에서 가져와서
            if (i < 4)                                              // 0~3 까지는
            {
                GetIn(temp.gameObject, enemyPos[i]);                // 슬롯 위치에 보여지게 넣고
                GameManager.Instance.SlotController.EnemySlot[i].AssignData(temp);
            }
            else
            {
                GetIn(temp.gameObject, enemyStandbyPos[i - 4]);         // 넘어간 것들은 대기석에 넣기
                GameManager.Instance.SlotController.EnemyStandbySlot[i - 4].AssignData(temp);
            }
        }
    }

    void GetIn(GameObject actor, Transform pos)
    {
        actor.transform.SetParent(pos);
        actor.transform.localPosition = Vector3.zero;
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
