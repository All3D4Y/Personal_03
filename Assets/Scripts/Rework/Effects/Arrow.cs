using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : RecycleObject
{
    public float moveSpeed = 3.0f;

    public bool isRight = false;

    public int IsRight
    {
        get => isRight ? -1 : 1;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        DisableTimer(0.35f);
    }

    void Update()
    {
        transform.Translate(Time.deltaTime * moveSpeed * Vector2.left * IsRight);
    }
}
