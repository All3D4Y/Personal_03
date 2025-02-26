using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundManager : MonoBehaviour
{
    SpriteRenderer sR;
    void Awake()
    {
        sR = GetComponent<SpriteRenderer>();
    }

    public void SetBackGround(Sprite sprite)
    {
        sR.sprite = sprite;
    }
}
