using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityDataManager : MonoBehaviour
{
    public abstract EntityData this[uint index]
    {
        get; set;
    }
}
