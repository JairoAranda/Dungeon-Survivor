using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [Header("Input Settings")]
    [Space]
    [Tooltip("Horizontal and Vertical input axis names.")]
    [SerializeField] InputActionAsset inputActions;

    [SerializeField] InputActionEnum action;

    private InputAction moveAction;

    private void OnEnable()
    {
        var gameplayMap = inputActions.FindActionMap("Gameplay");
        moveAction = gameplayMap.FindAction(action.ToString());
        moveAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
    }

    public Vector2 GetMovementInput()
    {
        // Obtiene la entrada horizontal y vertical basándose en los nombres de los ejes
        Vector2 moveInput = moveAction.ReadValue<Vector2>();

        // Devuelve un Vector2 que contiene el movimiento en ambas direcciones, normalizado para mantener una velocidad uniforme en todas las direcciones
        return new Vector2(moveInput.x, moveInput.y).normalized;
    }
}
