using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyDataManager : ActorDataManager
{
    public Ally[] allies;
    public override Actor this[uint index] 
    { 
        get => allies[index];
        set => allies[index] = value as Ally; 
    }
}
