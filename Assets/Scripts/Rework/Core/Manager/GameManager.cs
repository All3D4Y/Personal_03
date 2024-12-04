using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    SlotTransformContainer slotTransform;

    public SlotTransformContainer SlotTransform => slotTransform;

    protected override void OnInitialize()
    {
        slotTransform = FindAnyObjectByType<SlotTransformContainer>();
    }
}
