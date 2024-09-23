using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPosition : MonoBehaviour
{
    [SerializeField] Transform handPosition; // Transform público para establecer la referencia a la posición de la mano

    void Update()
    {
        transform.position = handPosition.position; // Actualiza la posición del objeto a la posición del objeto 'handPosition'
    }
}
