using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageDataManager : Singleton<StageDataManager>
{
    StageData currentStage;

    public StageData[] stageDatas;

    public StageData CurrentStage 
    { 
        get => currentStage; 
        set 
        { 
            currentStage = value; 
        } 
    }
}
