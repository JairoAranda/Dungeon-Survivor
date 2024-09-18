using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class HandMovement : EnemyDetector
{
    [HideInInspector]
    public bool canAim = true; // Variable pública que controla si el jugador puede apuntar

    protected override void Start()
    {
        // Se establece un rango de detección infinito, para siempre detectar enemigos
        detectionRange = float.PositiveInfinity;

        // Se habilita la capacidad de apuntar
        canAim = true;
    }

    void Update()
    {
        // Si está permitido apuntar
        if (canAim)
        {
            // Verifica si el modo automático está activado
            if (OptionManager.instance.isAuto)
            {
                AutoAim();
            }
            else
            {
                MouseAim();
            }
        }
        
    }

    void AutoAim()
    {
        // Detecta el enemigo más cercano usando el sistema de detección de la clase base EnemyDetector
        Transform closestEnemy = DetectClosestEnemy();

        if (closestEnemy != null)
        {
            // Calcula la dirección del enemigo
            Vector3 direction = closestEnemy.position - transform.position;

            // Calcula el ángulo entre el jugador y el enemigo
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Aplica la rotación al objeto basado en el ángulo calculado
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
       
    }

    void MouseAim()
    {
        // Obtiene la posición del mouse en coordenadas del mundo
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // Asegura que la coordenada Z del mouse esté en 0 (2D)

        // Calcula la dirección entre el objeto y la posición del mouse
        Vector3 direction = mousePosition - transform.position;

        // Calcula el ángulo entre el objeto y el mouse
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Aplica la rotación al objeto basado en el ángulo calculado
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
