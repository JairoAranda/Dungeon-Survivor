using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPosition : MonoBehaviour
{
    [SerializeField] Transform handPosition; // Transform p�blico para establecer la referencia a la posici�n de la mano

    void Update()
    {
        transform.position = handPosition.position; // Actualiza la posici�n del objeto a la posici�n del objeto 'handPosition'
    }
}
