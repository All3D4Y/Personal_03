using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentPositionMark : MonoBehaviour
{
    public float flashSpeed = 0.1f;
    public float moveSpeed = 0.1f;

    Image mark;

    float elapsedTime = 0;

    bool canClick;

    public Action onMarkMoveEnd;

    void Awake()
    {
        mark = GetComponentInChildren<Image>();
        canClick = true;
    }

    void Update()
    {
        elapsedTime += flashSpeed * Time.deltaTime;
        mark.color = new Color(1, 1, 1, 0.083f * (Mathf.Cos(elapsedTime) + 11));
    }

    public void OnMoveMark(MapPoint mapPoint)
    {
        if (canClick)
        {
            canClick = false;
            StopAllCoroutines();
            StartCoroutine(OnMoveMarkCoroutine(mapPoint)); 
        }
    }

    IEnumerator OnMoveMarkCoroutine(MapPoint mapPoint)
    {
        while (Vector3.SqrMagnitude(mapPoint.transform.position - transform.position) > 0.25f)
        {
            transform.position += moveSpeed * Time.deltaTime * (mapPoint.transform.position - transform.position).normalized;
            yield return null;
        }
        transform.position = mapPoint.transform.position;
        onMarkMoveEnd?.Invoke();
        canClick = true;
    }
}
