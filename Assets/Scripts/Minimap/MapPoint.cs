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

    Image pointImage;
    Image goalMark;
    Image battleMark;
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
            SceneManager.LoadScene("TestBattle");
            Debug.Log($"{stageData.name} 전투 시작...");
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
