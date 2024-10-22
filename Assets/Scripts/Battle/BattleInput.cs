using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BattleInput : MonoBehaviour
{
    InputActions inputActions;

    public Action<int> onScroll;

    void Awake()
    {
        inputActions = new InputActions();
    }

    void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Scroll.performed += OnScroll;
    }

    void OnDisable()
    {
        inputActions.Player.Scroll.performed -= OnScroll;
        inputActions.Player.Disable();
    }

    private void OnScroll(InputAction.CallbackContext context)
    {
        float input = context.ReadValue<float>();
        if (input > 0.0f)
        {
            onScroll?.Invoke(1);
        }
        else if (input < 0.0f)
        {
            onScroll?.Invoke(-1);
        }
    }
}
