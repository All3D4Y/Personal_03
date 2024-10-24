using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageDataManager : MonoBehaviour
{
    public StageData[] stageDatas;

    public OnFieldAllies onFieldAllies;

    public Ally[] Allies 
    {
        get => onFieldAllies.allies;
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
    
        Ally[] allyDatas = null;
        Enemy[] enemyDatas = null;

        allyDatas = onFieldAllies.allies;
    
        StageData stageData = stageDatas[stageIndex];
        enemyDatas = stageData.enemyDatas;
    
        GameManager.Instance.BattleManager.SlotController.InitialAssign(allyDatas, enemyDatas);
    }
}
