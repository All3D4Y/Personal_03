using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageDataManager : MonoBehaviour
{
    public StageData[] stageDatas;

    public OnFieldAllies onFieldAllies;

    uint currentStageIndex = 0;

    public List<Ally> CurrentStageAllyList { get; set; }
    public List<Enemy> CurrentStageEnemyList { get; set; }

    public Ally[] Allies 
    {
        get => onFieldAllies.allies;
    }

    public uint CurrentStageIndex
    {
        get => currentStageIndex;
        set => currentStageIndex = value;
    }

    public StageData this[uint index]
    {
        get => stageDatas[index];
        set => stageDatas[index] = value;
    }

    /// <summary>
    /// 스테이지 정보를 로드하는 함수
    /// </summary>
    /// <param name="stageIndex">스테이지의 인덱스</param>
    public void LoadStage(uint stageIndex)
    {
        Debug.Log("스테이지 정보 로드");

        CurrentStageIndex = stageIndex;

        GameManager.Instance.SlotVisualizer.Initialize();
    }
    public int[] GetStageAllyIndex()
    {
        int[] result = new int[Allies.Length];
        for (int i = 0; i < result.Length; i++)
        {
            result[i] = (int)Allies[i].Code;
        }
        return result;
    }

    public int[] GetStageEnemyIndex(uint stageIndex)
    {
        int[] result = new int[stageDatas[stageIndex].enemyDatas.Length];

        for(int i = 0; i < result.Length; i++)
        {
            result[i] = (int)stageDatas[stageIndex].enemyDatas[i].Code;
        }
        return result;
    }
}
