using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


public class RebindingButton : MonoBehaviour
{
    [SerializeField] InputActionAsset inputActions; // Referencia al InputAction desde el Inspector
    private InputAction rebindAction; // Acción a reasignar

    public void StartRebindingMovement(MoveActionsEnum direction)
    {
        var gameplayMap = inputActions.FindActionMap("Gameplay");  // Encuentra el ActionMap del Gameplay
        rebindAction = gameplayMap.FindAction("Move");             // Encuentra la acción "Move" (Vector2)

        // Determina cuál de los bindings (eje X o eje Y) se va a reasignar
        int bindingIndex = -1;
        if (direction.ToString() == "Up")
            bindingIndex = rebindAction.bindings.IndexOf(x => x.name == "up");
        else if (direction.ToString() == "Down")
            bindingIndex = rebindAction.bindings.IndexOf(x => x.name == "down");
        else if (direction.ToString() == "Left")
            bindingIndex = rebindAction.bindings.IndexOf(x => x.name == "left");
        else if (direction.ToString() == "Right")
            bindingIndex = rebindAction.bindings.IndexOf(x => x.name == "right");

        if (bindingIndex == -1)
        {
            Debug.LogError("No se encontró la dirección especificada para el movimiento.");
            return;
        }

        // Inicia el proceso de reasignación para la dirección seleccionada
        rebindAction.PerformInteractiveRebinding(bindingIndex)
            .WithControlsExcluding("<Mouse>/position")
            .WithControlsExcluding("<Mouse>/delta")
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation =>
            {
                // Limpia la operación y habilita la acción con el nuevo binding
                rebindAction.Enable();
                operation.Dispose();  // Limpia la operación de reasignación
            })
            .Start();  // Inicia la reasignación interactiva

        PlayerPrefs.SetString(direction + "_Binding", rebindAction.bindings[bindingIndex].effectivePath);
    }

    public void StartRebinding(InputActionEnum actionName)
    {
        // Encuentra la acción que queremos reasignar
        var gameplayMap = inputActions.FindActionMap("Gameplay");  // Encuentra el ActionMap correspondiente
        rebindAction = gameplayMap.FindAction(actionName.ToString());         // Encuentra la acción por nombre (ej. "Shoot")

        // Inicia el proceso de reasignación de la acción
        rebindAction.Disable();  // Desactiva la acción mientras se reasigna

        rebindAction.PerformInteractiveRebinding()
            .WithControlsExcluding("<Mouse>/position") // Excluye el movimiento del ratón
            .WithControlsExcluding("<Mouse>/delta")    // Excluye el delta del ratón
            .OnMatchWaitForAnother(0.1f)              // Evita detectar dos entradas de golpe
            .OnComplete(operation =>
            {
                // Limpia el proceso y habilita la acción con la nueva tecla asignada
                rebindAction.Enable();
                operation.Dispose();  // Limpia la operación
            })
            .Start();  // Inicia el proceso de rebinding

        PlayerPrefs.SetString(actionName + "_Binding", rebindAction.bindings[0].effectivePath);
    }

}
