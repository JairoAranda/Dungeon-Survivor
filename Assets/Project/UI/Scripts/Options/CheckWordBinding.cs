using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class CheckWordBinding : MonoBehaviour
{
    TextMeshProUGUI textButton;

    public MoveActionsEnum moveActionsEnum;

    public InputActionEnum inputActionEnum;

    public bool isDirection;

    private void Start()
    {
        textButton = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (isDirection) BindingAction(moveActionsEnum.ToString());
        else BindingAction(inputActionEnum.ToString());
    }

    void BindingAction(string direction)
    {
        // Si existe un binding guardado en PlayerPrefs, lo utilizamos
        if (PlayerPrefs.HasKey(direction + "_Binding"))
        {
            string savedBinding = PlayerPrefs.GetString(direction + "_Binding");

            // Convertir la ruta guardada a un formato legible para el jugador
            InputBinding binding = new InputBinding { path = savedBinding };
            string displayString = binding.ToDisplayString();

            // Asigna el binding legible al texto
            textButton.text = displayString;
        }
    }
}
