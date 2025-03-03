using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Route
{
    None = 0,
    A,
    B,
    C
}

public class MapPoint : MonoBehaviour
{
    public StageData stageData;
    public bool isStart;
    public bool isGoal;
    public int index;
    public Route route = Route.None;

    Image pointImage;
    Image goalMark;
    Button button;

    bool isMoved = false;
    Color invalidColor = new Color(0.5f, 0.5f, 0.5f);

    public Action<MapPoint> onClickPoint;
    public bool IsMoved => isMoved;
    public bool IsCurrent { get; set; }

    void Awake()
    {
        pointImage = GetComponent<Image>();
        goalMark = transform.GetChild(0).GetComponent<Image>();
        button = GetComponent<Button>();

        button.onClick.AddListener(OnMovePoint);

        ValidPoint(false);
    }

    void Reset()
    {
        if (isGoal)
            goalMark.enabled = true;
        else
            goalMark.enabled = false;
        if (isStart)
            IsCurrent = true;
    }

    public void OnMovePoint()
    {
        onClickPoint?.Invoke(this);

        if (stageData == null)
        {
            // 이동
        }
        else
        {
            // 전투씬 로드
        }
        isMoved = true;
    }

    public void ValidPoint(bool isValid)
    {
        if (!isValid)
        {
            pointImage.color = invalidColor;
            button.enabled = false;
        }
        else
        {
            pointImage.color = Color.white;
            button.enabled = true;
        }
    }
}
