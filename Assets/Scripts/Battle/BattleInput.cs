using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BattleInput : MonoBehaviour
{
    InputActions inputActions;

    public float coolTime = 0.1f;
    float elapsedTime = 0.0f;

    public Action<int> onScroll;

    void Awake()
    {
        inputActions = new InputActions();
        elapsedTime = coolTime;
    }

    void Update()
    {
        elapsedTime -= Time.deltaTime;
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

        if (elapsedTime < 0)
        {
            if (input > 0.0f)
            {
                onScroll?.Invoke(1);
                elapsedTime = coolTime;
            }
            else if (input < 0.0f)
            {
                onScroll?.Invoke(-1);
                elapsedTime = coolTime;
            }
        }
    }
}
