using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentButtonColor : MonoBehaviour
{
    [SerializeField] private Color color; // Color que se aplicará al botón actual

    [SerializeField] Image[] buttons; // Arreglo de botones cuyo color se modificará

    // Cambia el color de los botones para destacar el botón actual.
    public void ChangeColorButton(Image currentButton)
    {
        // Restablece el color de todos los botones a blanco
        foreach (var button in buttons)
        {
            button.color = Color.white;
        }

        // Aplica el color especificado al botón actual
        currentButton.color = color;
    }
}
