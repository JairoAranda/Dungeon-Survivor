using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class MoneyText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI money; // Referencia al componente TextMeshProUGUI que muestra la cantidad de dinero.

    // Actualiza el texto con la cantidad actual de dinero del jugador.
    private void FixedUpdate()
    {
        money.text = MoneyManager.instance.money + " $";
    }
}
