using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lines : MonoBehaviour
{
    public Vector2 preset;

    LineRenderer lineRenderer;
    List<MapPoint> points;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        points = new List<MapPoint>();
        for (int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            points.Add(transform.GetChild(0).GetChild(i).GetComponent<MapPoint>());
        }
    }

    void DrawLine()
    {
        lineRenderer.positionCount = points.Count;
        MapPoint current = points.Find(p => p.isStart);
        lineRenderer.SetPosition(0, current.transform.position);

        while (points.Find(p => p.index == current.index + 1) == null)
        {
            List<MapPoint> list = points.FindAll(p => p.index == current.index + 1);
            if (list.Count == 1)
            {

            }
        }
    }
}
