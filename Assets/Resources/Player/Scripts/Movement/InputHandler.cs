using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [Header("Input Settings")]
    [Space]
    [Tooltip("Horizontal and Vertical input axis names.")]
    public string horizontalAxis = "Horizontal"; // Nombre del eje para el movimiento horizontal
    public string verticalAxis = "Vertical"; // Nombre del eje para el movimiento vertical

    public Vector2 GetMovementInput()
    {
        // Obtiene la entrada horizontal y vertical basándose en los nombres de los ejes
        float moveHorizontal = Input.GetAxisRaw(horizontalAxis);
        float moveVertical = Input.GetAxisRaw(verticalAxis);

        // Devuelve un Vector2 que contiene el movimiento en ambas direcciones, normalizado para mantener una velocidad uniforme en todas las direcciones
        return new Vector2(moveHorizontal, moveVertical).normalized;
    }
}
