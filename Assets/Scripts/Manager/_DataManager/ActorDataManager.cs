using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActorDataManager : MonoBehaviour
{
    public abstract Actor this[uint index]
    {
        get; set;
    }
}
