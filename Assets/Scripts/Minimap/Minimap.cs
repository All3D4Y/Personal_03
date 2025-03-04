using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    List<MapPoint> points;
    CurrentPositionMark mark;

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

        if (GameManager.Instance.CurrentPosition == null)
        {
            points.Find(p => p.isStart).IsCurrent = true;
        }
        else
        {
            Initialize();
        }

        if (points.Find(p => p.IsCurrent) != null)
            mark.transform.position = points.Find(p => p.IsCurrent).transform.position;

        ValidPoint();
    }

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

    public void OnMoveCurrentMark(MapPoint point)
    {
        points.Find(p => p.IsCurrent).IsCurrent = false;
        mark.OnMoveMark(point);
        point.IsCurrent = true;
    }

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
}
