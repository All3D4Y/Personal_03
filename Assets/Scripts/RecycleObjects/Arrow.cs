using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : RecycleObject
{
    public float moveSpeed = 3.0f;
    void Start()
    {
        DisableTimer(0.2f);
    }

    void Update()
    {
        transform.Translate(Time.deltaTime * moveSpeed * Vector2.left);
    }
}
