using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMovement : MonoBehaviour
{
    void Update()
    {
        // Obtener la posici�n del rat�n en el mundo
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // Asegurarse de que la Z est� en el mismo plano que el objeto

        // Calcular la direcci�n del rat�n desde el objeto
        Vector3 direction = mousePosition - transform.position;

        // Calcular el �ngulo de rotaci�n en radianes
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Rotar el objeto hacia el rat�n en el eje Z
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
