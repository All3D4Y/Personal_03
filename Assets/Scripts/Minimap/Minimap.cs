using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    List<MapPoint> points;
    LineRenderer lineRenderer;
    CurrentPositionMark mark;

    void Awake()
    {
        // points 초기화하기
        lineRenderer = GetComponent<LineRenderer>();
        mark = GetComponentInChildren<CurrentPositionMark>();
    }

    void Start()
    {
        OnMoveCurrentMark(points.Find(p => p.isStart));
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

            }
        }
        else if (current.route == Route.B)
        {

        }
        else
        {

        }
    }
}
