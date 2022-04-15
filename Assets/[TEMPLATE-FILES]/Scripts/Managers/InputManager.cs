using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private void Awake()
    {
        EventManager.OnInputManagerCreated?.Invoke(this);
    }

    private void Update()
    {
        InputListener();
    }

    private void InputListener()
    {
        if (Input.anyKey)
        {
            EventManager.OnAnyKeyPressed?.Invoke();
        }
    }
}
