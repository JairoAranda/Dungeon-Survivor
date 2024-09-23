using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LoadBindings : MonoBehaviour
{
    [SerializeField] InputActionAsset inputActions;

    InputActionEnum[] actions = (InputActionEnum[])Enum.GetValues(typeof(InputActionEnum));
    MoveActionsEnum[] directions = (MoveActionsEnum[])Enum.GetValues(typeof(MoveActionsEnum));

    void Start()
    {
        foreach (InputActionEnum action in actions)
        {
            LoadBinding(action.ToString());
        }

        foreach (MoveActionsEnum direction in directions)
        {
            LoadMovementBinding(direction.ToString());
        }
    }

    public void LoadBinding(string actionName)
    {
        var gameplayMap = inputActions.FindActionMap("Gameplay");
        var action = gameplayMap.FindAction(actionName);

        if (PlayerPrefs.HasKey(actionName + "_Binding"))
        {
            string savedBinding = PlayerPrefs.GetString(actionName + "_Binding");
            action.ApplyBindingOverride(0, savedBinding);  // Aplica el binding guardado
        }
    }

    public void LoadMovementBinding(string direction)
    {
        var gameplayMap = inputActions.FindActionMap("Gameplay");
        var action = gameplayMap.FindAction("Move");

        if (PlayerPrefs.HasKey(direction + "_Binding"))
        {
            string savedBinding = PlayerPrefs.GetString(direction + "_Binding");
            int bindingIndex = -1;
            if (direction == "Up")
                bindingIndex = action.bindings.IndexOf(x => x.name == "up");
            else if (direction == "Down")
                bindingIndex = action.bindings.IndexOf(x => x.name == "down");
            else if (direction == "Left")
                bindingIndex = action.bindings.IndexOf(x => x.name == "left");
            else if (direction == "Right")
                bindingIndex = action.bindings.IndexOf(x => x.name == "right");

            action.ApplyBindingOverride(bindingIndex, savedBinding);
        }
    }
}
