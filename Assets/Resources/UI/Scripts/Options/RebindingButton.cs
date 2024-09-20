using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


public class RebindingButton : MonoBehaviour
{
    [SerializeField] InputActionAsset inputActions; // Referencia al InputAction desde el Inspector
    private InputAction rebindAction; // Acci�n a reasignar

    public void StartRebindingMovement(MoveActionsEnum direction)
    {
        var gameplayMap = inputActions.FindActionMap("Gameplay");  // Encuentra el ActionMap del Gameplay
        rebindAction = gameplayMap.FindAction("Move");             // Encuentra la acci�n "Move" (Vector2)

        // Determina cu�l de los bindings (eje X o eje Y) se va a reasignar
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
            Debug.LogError("No se encontr� la direcci�n especificada para el movimiento.");
            return;
        }

        // Inicia el proceso de reasignaci�n para la direcci�n seleccionada
        rebindAction.PerformInteractiveRebinding(bindingIndex)
            .WithControlsExcluding("<Mouse>/position")
            .WithControlsExcluding("<Mouse>/delta")
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation =>
            {
                // Limpia la operaci�n y habilita la acci�n con el nuevo binding
                rebindAction.Enable();
                operation.Dispose();  // Limpia la operaci�n de reasignaci�n
            })
            .Start();  // Inicia la reasignaci�n interactiva

        PlayerPrefs.SetString(direction + "_Binding", rebindAction.bindings[bindingIndex].effectivePath);
    }

    public void StartRebinding(InputActionEnum actionName)
    {
        // Encuentra la acci�n que queremos reasignar
        var gameplayMap = inputActions.FindActionMap("Gameplay");  // Encuentra el ActionMap correspondiente
        rebindAction = gameplayMap.FindAction(actionName.ToString());         // Encuentra la acci�n por nombre (ej. "Shoot")

        // Inicia el proceso de reasignaci�n de la acci�n
        rebindAction.Disable();  // Desactiva la acci�n mientras se reasigna

        rebindAction.PerformInteractiveRebinding()
            .WithControlsExcluding("<Mouse>/position") // Excluye el movimiento del rat�n
            .WithControlsExcluding("<Mouse>/delta")    // Excluye el delta del rat�n
            .OnMatchWaitForAnother(0.1f)              // Evita detectar dos entradas de golpe
            .OnComplete(operation =>
            {
                // Limpia el proceso y habilita la acci�n con la nueva tecla asignada
                rebindAction.Enable();
                operation.Dispose();  // Limpia la operaci�n
            })
            .Start();  // Inicia el proceso de rebinding

        PlayerPrefs.SetString(actionName + "_Binding", rebindAction.bindings[0].effectivePath);
    }

}
