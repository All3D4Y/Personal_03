using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageDataManager : MonoBehaviour
{
    public StageData[] stageDatas;

    public StageData this[uint index]
    {
        get => stageDatas[index];
        set => stageDatas[index] = value;
    }
}
