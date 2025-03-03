using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestMap : TestBase
{
    public MapPoint point1;
    public MapPoint point2;
    public MapPoint point3;

    Minimap map;

    void Start()
    {
        map = FindAnyObjectByType<Minimap>();
    }

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        map.OnMoveCurrentMark(point1);
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        map.OnMoveCurrentMark(point2);
    }

    protected override void OnTest3(InputAction.CallbackContext context)
    {
        map.OnMoveCurrentMark(point3);
    }
}
