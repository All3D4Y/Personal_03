using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 현재 위치를 기록해두기 위한 구조체
/// </summary>
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

/// <summary>
/// 갈래길이 있을 때 어떤 길인지 확인하기 위한 열거형
/// </summary>
public enum Route
{
    None = 0,
    A,
    B,
    C
}

public class MapPoint : MonoBehaviour
{
    public StageData stageData;         // 이 지점에 전투가 있을 때 전투의 데이터
    public bool isStart;                // 시작 지점인지
    public bool isGoal;                 // 목표 지점인지
    public int index;                   // 지점의 인덱스
    public Route route = Route.None;    // 갈래길이 있을 경우 어느 길 위인지 확인하기 위함
    [Header("Line")]
    public Vector2 preset;              // 선 그리기의 정점 조절을 위한 프리셋
    public float lineWidth;             // 선의 두께

    Image pointImage;
    Image goalMark;
    Image battleMark;
    Button button;
    LineRenderer lineRenderer;

    bool isMoved = false;
    Color invalidColor = new Color(0.5f, 0.5f, 0.5f);

    public Action<MapPoint> onClickPoint;   // 누른 지점을 전달하는 델리게이트
    public bool IsMoved => isMoved;         // 지나간 지점인지 확인하는 프로퍼티
    public bool IsCurrent { get; set; }     // 현재 위치인지 확인하는 프로퍼티

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

    /// <summary>
    /// 지점을 클릭했을 때 호출되는 함수 (이동 및 전투 로드)
    /// </summary>
    public void OnMovePoint()
    {
        onClickPoint?.Invoke(this); // 이 지점으로 이동했다고 알림, 이동처리

        if (stageData != null)      // 이 지점에 전투 정보가 있다면 전투씬 로드
        {
            // 전투씬 로드
            GameManager.Instance.CurrentPosition = new CurrentPosition(index, route);
            StageDataManager.Instance.CurrentStage = stageData;
            LoadSceneManager.Instance.LoadScene(2);
        }
        isMoved = true;
    }

    /// <summary>
    /// 현재 위치에서 이동 가능한 지점을 하이라이트 하는 함수
    /// </summary>
    /// <param name="isValid"></param>
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

    /// <summary>
    /// 길을 잇는 선을 그리는 함수
    /// </summary>
    /// <param name="pointPosition">이 지점과 이을 점의 위치</param>
    public void DrawLine(Vector2 pointPosition)
    {
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.positionCount = 2;

        lineRenderer.SetPosition(0, (Vector2)transform.position + preset);
        lineRenderer.SetPosition(1, pointPosition + preset);
    }
    
    /// <summary>
    /// 갈래길이 있을 경우 선 여러개 그리기
    /// </summary>
    /// <param name="points"></param>
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
