using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [Header("Input Settings")]
    [Space]
    [Tooltip("Horizontal and Vertical input axis names.")]
    public string horizontalAxis = "Horizontal";
    public string verticalAxis = "Vertical";

    public Vector2 GetMovementInput()
    {
        float moveHorizontal = Input.GetAxisRaw(horizontalAxis);
        float moveVertical = Input.GetAxisRaw(verticalAxis);
        return new Vector2(moveHorizontal, moveVertical).normalized;
    }
}
