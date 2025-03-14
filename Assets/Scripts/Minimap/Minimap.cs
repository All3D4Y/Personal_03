using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    List<MapPoint> points;      // 지점들
    CurrentPositionMark mark;   // 현재 위치 표시 마크

    void Awake()
    {
        // points 초기화하기
        points = new List<MapPoint>();

        Transform child = transform.GetChild(0);
        for (int i = 0; i < child.childCount; i++)
        {
            points.Add(child.GetChild(i).GetComponent<MapPoint>());
        }

        mark = GetComponentInChildren<CurrentPositionMark>();
    }

    void Start()
    {
        mark.onMarkMoveEnd -= ValidPoint;
        mark.onMarkMoveEnd += ValidPoint;
        foreach (MapPoint p in points)
        {
            p.onClickPoint -= OnMoveCurrentMark;
            p.onClickPoint += OnMoveCurrentMark;
        }

        if (GameManager.Instance.CurrentPosition == null)   // GameManager에 현재 위치가 null 이다 -> 게임을 처음 시작했다
        {                                                 
            points.Find(p => p.isStart).IsCurrent = true;   // 시작 위치를 현재 위치로
        }                                                 
        else                                                // null이 아니다 -> 진행 한 적이 있다
        {
            Initialize();                                   // 초기화 실행
        }

        if (points.Find(p => p.IsCurrent) != null)
            mark.transform.position = points.Find(p => p.IsCurrent).transform.position;

        ValidPoint();
        DrawLines();
    }

    /// <summary>
    /// 초기화
    /// </summary>
    public void Initialize()
    {
        if (GameManager.Instance.CurrentPosition != null)
        {
            CurrentPosition current = (CurrentPosition)GameManager.Instance.CurrentPosition;

            List<MapPoint> temp = points.FindAll(p => p.index == current.index);
            MapPoint p;
            if (temp.Count == 1)
                p = temp[0];
            else
                p = temp.Find(p => p.route == current.route);
            p.IsCurrent = true; 
        }
    }

    /// <summary>
    /// 현재 위치 표시 UI를 이동시키는 함수
    /// </summary>
    /// <param name="point">이동할 지점</param>
    public void OnMoveCurrentMark(MapPoint point)
    {
        points.Find(p => p.IsCurrent).IsCurrent = false;
        mark.OnMoveMark(point);
        point.IsCurrent = true;
    }

    /// <summary>
    /// 현재 위치에서 이동 가능한 지점을 하이라이트 표시
    /// </summary>
    public void ValidPoint()
    {
        MapPoint current = points.Find(p => p.IsCurrent);
        List<MapPoint> list = points.FindAll(p => p.index == current.index + 1);
        if (list.Count != 0)
        {
            if (current.route == Route.None)
            {
                foreach (MapPoint p in list)
                {
                    p.ValidPoint(true);
                }
            }
            else if (current.route == Route.A)
            {
                if (list.Count == 1)
                {
                    foreach (MapPoint p in list)
                    {
                        p.ValidPoint(true);
                    }
                }
                else
                {
                    foreach (MapPoint p in list)
                    {
                        if (p.route == Route.A)
                            p.ValidPoint(true);
                        else
                            p.ValidPoint(false);
                    }
                }
            }
            else if (current.route == Route.B)
            {
                if (list.Count == 1)
                {
                    foreach (MapPoint p in list)
                    {
                        p.ValidPoint(true);
                    }
                }
                else
                {
                    foreach (MapPoint p in list)
                    {
                        if (p.route == Route.B)
                            p.ValidPoint(true);
                        else
                            p.ValidPoint(false);
                    }
                }
            }
            else
            {
                if (list.Count == 1)
                {
                    foreach (MapPoint p in list)
                    {
                        p.ValidPoint(true);
                    }
                }
                else
                {
                    foreach (MapPoint p in list)
                    {
                        if (p.route == Route.C)
                            p.ValidPoint(true);
                        else
                            p.ValidPoint(false);
                    }
                }
            }

            foreach (MapPoint p in points.Except(list))
            {
                p.ValidPoint(false);
            }
        }
    }

    /// <summary>
    /// 길을 표시하기 위해 지점들 사이에 선을 그리는 함수
    /// </summary>
    public void DrawLines()
    {
        foreach (MapPoint point in points)
        {
            if (!point.isStart)
            {
                List<MapPoint> temp = points.FindAll(p => p.index == point.index - 1);
                if (temp.Count == 1)
                    point.DrawLine((Vector2)temp[0].transform.position);
                else
                {
                    if (point.route != Route.None)
                        point.DrawLine((Vector2)temp.Find(p => p.route == point.route).transform.position);
                    else
                        point.DrawMultiLine(temp);
                }
            }
        }
    }
}
