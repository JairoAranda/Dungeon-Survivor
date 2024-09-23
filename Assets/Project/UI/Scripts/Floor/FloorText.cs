using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloorText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI floor; // Texto que muestra el número de piso actual.
    [SerializeField] private TextMeshProUGUI timer; // Texto que muestra el tiempo restante en el piso actual.

    void Start()
    {
        // Muestra el número del piso actual
        floor.text = "Floor : " + RoundTimerManager.instance.currentFloor;
    }

    void Update()
    {
        // Actualiza el temporizador con el tiempo restante del piso actual
        timer.text = RoundTimerManager.instance.floorTimeRemaining.ToString();
    }
}
