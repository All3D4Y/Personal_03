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

    /// <summary>
    /// 배경 설정하는 함수
    /// </summary>
    /// <param name="sprite"></param>
    public void SetBackGround(Sprite sprite)
    {
        sR.sprite = sprite;
    }
}
