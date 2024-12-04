using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotTransformContainer : MonoBehaviour
{
    Transform[] playerSlotTransforms;
    Transform[] enemySlotTransforms;

    public Transform[] PlayerSlot => playerSlotTransforms;
    public Transform[] EnemySlot => enemySlotTransforms;

    void Awake()
    {
        playerSlotTransforms = new Transform[8];                            // 플레이어 슬롯
        Transform child = transform.GetChild(0);
        for (int i = 0; i < playerSlotTransforms.Length; i++)
        {
            playerSlotTransforms[i] = child.GetChild(i);
        }

        enemySlotTransforms = new Transform[8];                             // 적 슬롯
        child = transform.GetChild(1);
        for (int i = 0; i < enemySlotTransforms.Length; i++)
        {
            enemySlotTransforms[i] = child.GetChild(i);
        }
    }
}
