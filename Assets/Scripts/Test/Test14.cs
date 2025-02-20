using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test14 : TestBase
{
    public Item heal;
    public Item mind;

    ItemManager manager;

    void Start()
    {
        manager = ItemManager.Instance;
    }

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        manager.AddNewItem(heal);
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        manager.GetItem(heal);
    }

    protected override void OnTest3(InputAction.CallbackContext context)
    {
        manager.AddNewItem(mind);
    }

    protected override void OnTest4(InputAction.CallbackContext context)
    {
        manager.UseItem(heal);
    }

    protected override void OnTest5(InputAction.CallbackContext context)
    {
        manager.UseItem(mind);
    }

    protected override void OnTestLClick(InputAction.CallbackContext context)
    {
        Debug.Log($"egg : {manager.Items[manager.egg]}");
        Debug.Log($"heal : {manager.Items[heal]}");
        Debug.Log($"mind : {manager.Items[mind]}");
    }
}
