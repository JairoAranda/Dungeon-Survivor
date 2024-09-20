using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

[CustomEditor(typeof(CheckWordBinding))]
public class CustomInspectorWordBinding : Editor
{

    public override void OnInspectorGUI()
    {
        // Obtiene la referencia del objeto inspeccionado
        CheckWordBinding checkWordBinding = (CheckWordBinding)target;

        // Muestra el campo bool en el inspector
        checkWordBinding.isDirection = EditorGUILayout.Toggle("Is Direction?", checkWordBinding.isDirection);

        // Según el valor de isDirection, muestra la variable adecuada
        if (checkWordBinding.isDirection)
        {
            checkWordBinding.moveActionsEnum = (MoveActionsEnum)EditorGUILayout.EnumPopup("Move Action", checkWordBinding.moveActionsEnum);
        }

        else
        {
            checkWordBinding.inputActionEnum = (InputActionEnum)EditorGUILayout.EnumPopup("Input Action", checkWordBinding.inputActionEnum);
        }

        // Aplica cualquier cambio realizado en el editor
        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
        }
    }
}
