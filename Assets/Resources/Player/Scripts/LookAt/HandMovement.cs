using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class HandMovement : EnemyDetector
{
    bool m_isAuto;

    protected override void Start()
    {
        m_isAuto = GetComponentInParent<ShootController>().isAuto;

        base.Start();
    }

    void Update()
    {
        if (m_isAuto)
        {
            AutoAim();
        }
        else
        {
            MouseAim();
        }
    }

    void AutoAim()
    {
        Transform closestEnemy = DetectClosestEnemy();

        if (closestEnemy != null)
        {
            Vector3 direction = closestEnemy.position - transform.position;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
       
    }

    void MouseAim()
    {
        // Obtener la posición del ratón en el mundo
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // Asegurarse de que la Z esté en el mismo plano que el objeto

        // Calcular la dirección del ratón desde el objeto
        Vector3 direction = mousePosition - transform.position;

        // Calcular el ángulo de rotación en radianes
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Rotar el objeto hacia el ratón en el eje Z
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
