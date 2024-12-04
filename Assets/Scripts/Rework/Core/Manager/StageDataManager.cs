using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageDataManager : Singleton<StageDataManager>
{
    public StageData[] stageDatas;

    public StageData CurrentStage { get; set; }
}
