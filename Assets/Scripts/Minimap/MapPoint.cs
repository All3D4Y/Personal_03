using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public struct CurrentPosition
{
    public int index;
    public Route route;

    public CurrentPosition(int index, Route route)
    {
        this.index = index;
        this.route = route;
    }
}

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
    [Header("Line")]
    public Vector2 preset;
    public float lineWidth;

    Image pointImage;
    Image goalMark;
    Image battleMark;
    Button button;
    LineRenderer lineRenderer;

    bool isMoved = false;
    Color invalidColor = new Color(0.5f, 0.5f, 0.5f);

    public Action<MapPoint> onClickPoint;
    public bool IsMoved => isMoved;
    public bool IsCurrent { get; set; }

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        pointImage = GetComponent<Image>();
        goalMark = transform.GetChild(0).GetComponent<Image>();
        battleMark = transform.GetChild(1).GetComponent<Image>();
        button = GetComponent<Button>();

        button.onClick.AddListener(OnMovePoint);

        ValidPoint(false);
    }

    void Start()
    {
        if (isGoal)
            goalMark.enabled = true;
        else
            goalMark.enabled = false;

        if (stageData == null)
            battleMark.enabled = false;
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
            GameManager.Instance.CurrentPosition = new CurrentPosition(index, route);
            StageDataManager.Instance.CurrentStage = stageData;
            LoadSceneManager.Instance.LoadScene(2);
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

    public void DrawLine(Vector2 pointPosition)
    {
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.positionCount = 2;

        lineRenderer.SetPosition(0, (Vector2)transform.position + preset);
        lineRenderer.SetPosition(1, pointPosition + preset);
    }
    
    public void DrawMultiLine(List<MapPoint> points)
    {
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.positionCount = points.Count * 2 - 1;

        for (int i = 0; i < points.Count * 2 - 1; i++)
        {
            if (i % 2 == 1)
            {
                lineRenderer.SetPosition(i, (Vector2)transform.position + preset);
            }
            else
            {
                lineRenderer.SetPosition(i, (Vector2)points[i / 2].transform.position + preset);
            }
        }
    }
}
