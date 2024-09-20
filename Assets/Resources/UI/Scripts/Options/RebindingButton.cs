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

    [SerializeField] GameObject rebind; // UI que se activa durante el rebinding

    [SerializeField] GameObject used;

    public void StartRebindingMovement(string direction)
    {
        var gameplayMap = inputActions.FindActionMap("Gameplay");  // Encuentra el ActionMap del Gameplay
        rebindAction = gameplayMap.FindAction("Move");             // Encuentra la acción "Move" (Vector2)

        //Activar ui
        rebind.SetActive(true);

        // Determina cuál de los bindings (eje X o eje Y) se va a reasignar
        int bindingIndex = -1;
        if (direction == "Up")
            bindingIndex = rebindAction.bindings.IndexOf(x => x.name == "up");
        else if (direction == "Down")
            bindingIndex = rebindAction.bindings.IndexOf(x => x.name == "down");
        else if (direction == "Left")
            bindingIndex = rebindAction.bindings.IndexOf(x => x.name == "left");
        else if (direction == "Right")
            bindingIndex = rebindAction.bindings.IndexOf(x => x.name == "right");

        if (bindingIndex == -1)
        {
            Debug.LogError("No se encontró la dirección especificada para el movimiento.");
            return;
        }

        // Deshabilitar la acción antes de reasignar
        rebindAction.Disable();

        // Inicia el proceso de reasignación para la dirección seleccionada
        rebindAction.PerformInteractiveRebinding(bindingIndex)
            .WithControlsExcluding("<Mouse>/position")
            .WithControlsExcluding("<Mouse>/delta")
            .OnMatchWaitForAnother(0.1f)
            .OnPotentialMatch(operation =>
            {
                string newBinding = operation.selectedControl.path;

                if (IsBindingInUse(newBinding))
                {
                    StopAllCoroutines();
                    StartCoroutine(AlreadyUsed());
                    rebind.SetActive(false);
                    operation.Cancel();
                }
            })
            .OnComplete(operation =>
            {
                string newBinding = rebindAction.bindings[bindingIndex].effectivePath;

                // Limpia la operación y habilita la acción con el nuevo binding
                rebindAction.Enable();

                // Guardar la tecla reasignada en PlayerPrefs
                PlayerPrefs.SetString(direction + "_Binding", newBinding);

                //Desactivar ui
                rebind.SetActive(false);

                operation.Dispose();  // Limpia la operación de reasignación
            })
            .Start();  // Inicia la reasignación interactiva
    }

    public void StartRebinding(string actionName)
    {
        // Encuentra la acción que queremos reasignar
        var gameplayMap = inputActions.FindActionMap("Gameplay");  // Encuentra el ActionMap correspondiente
        rebindAction = gameplayMap.FindAction(actionName);         // Encuentra la acción por nombre (ej. "Shoot")

        //Activar ui
        rebind.SetActive(true);

        // Inicia el proceso de reasignación de la acción
        rebindAction.Disable();  // Desactiva la acción mientras se reasigna

        rebindAction.PerformInteractiveRebinding()
            .WithControlsExcluding("<Mouse>/position") // Excluye el movimiento del ratón
            .WithControlsExcluding("<Mouse>/delta")    // Excluye el delta del ratón
            .OnMatchWaitForAnother(0.1f)              // Evita detectar dos entradas de golpe
            .OnPotentialMatch(operation =>
            {
                string newBinding = operation.selectedControl.path;

                if (IsBindingInUse(newBinding))
                {
                    StopAllCoroutines();
                    StartCoroutine(AlreadyUsed());
                    rebind.SetActive(false);
                    operation.Cancel();
                }
            })
            .OnComplete(operation =>
            {
                string newBinding = rebindAction.bindings[0].effectivePath;

                // Limpia el proceso y habilita la acción con la nueva tecla asignada
                rebindAction.Enable();

                //Desactivar ui
                rebind.SetActive(false);

                // Guardar la tecla reasignada en PlayerPrefs
                PlayerPrefs.SetString(actionName + "_Binding", newBinding);
                operation.Dispose();  // Limpia la operación
            })
            .Start();  // Inicia el proceso de rebinding

    }

    IEnumerator AlreadyUsed()
    {
        used.SetActive(true);

        yield return new WaitForSeconds(3);

        used.SetActive(false);
    }

    private bool IsBindingInUse(string newBinding)
    {
        // Normalizar el binding nuevo
        string normalizedNewBinding = NormalizeBindingPath(newBinding);

        foreach (var action in inputActions)
        {
            foreach (var binding in action.bindings)
            {
                // Normalizar el binding actual para compararlo
                string normalizedBinding = NormalizeBindingPath(binding.effectivePath);

                if (normalizedBinding == normalizedNewBinding)
                {
                    return true;
                }
            }
        }
        return false;
    }

    // Función para normalizar el ControlPath
    private string NormalizeBindingPath(string path)
    {
        if (string.IsNullOrEmpty(path))
            return path;

        // Eliminar los símbolos "<" y ">" si están presentes al principio y al final de la ruta
        if (path.StartsWith("<") && path.EndsWith(">"))
        {
            path = path.Substring(1, path.Length - 2);
        }

        // Asegurarse de que el formato sea consistente sin duplicar barras "/"
        path = path.Replace("<", "/").Replace(">", "/");

        // Eliminar posibles dobles barras "//"
        path = path.Replace("//", "/");

        return path;
    }
}
