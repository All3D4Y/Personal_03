using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentPositionMark : MonoBehaviour
{
    public float flashSpeed = 0.1f;     // 마크가 점멸하는 속도
    public float moveSpeed = 0.1f;      // 마크가 이동하는 속도

    Image mark;

    float elapsedTime = 0;

    bool canClick;

    public Action onMarkMoveEnd;        // 마크의 이동이 끝남을 알리는 델리게이트

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

    /// <summary>
    /// 선택한 지점으로 마크를 이동시키는 함수
    /// </summary>
    /// <param name="mapPoint">선택한 지점</param>
    public void OnMoveMark(MapPoint mapPoint)
    {
        if (canClick)
        {
            canClick = false;
            StopAllCoroutines();
            StartCoroutine(OnMoveMarkCoroutine(mapPoint)); 
        }
    }

    /// <summary>
    /// 마크를 부드럽게 이동시키는 코루틴
    /// </summary>
    /// <param name="mapPoint">선택한 지점</param>
    /// <returns></returns>
    IEnumerator OnMoveMarkCoroutine(MapPoint mapPoint)
    {
        while (Vector3.SqrMagnitude(mapPoint.transform.position - transform.position) > 0.5f)
        {
            transform.position += moveSpeed * Time.deltaTime * (mapPoint.transform.position - transform.position).normalized;
            yield return null;
        }
        transform.position = mapPoint.transform.position;
        onMarkMoveEnd?.Invoke();
        canClick = true;
    }
}
