using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstShoot : BaseRangedAbility, IAbility
{
    [Header("Burst Config")]
    [Range(3, 6)]
    [SerializeField] int shoots = 4; // Cantidad de disparos por ráfaga
    [Range(0.1f, 1f)]
    [SerializeField] float delayShoot = 0.25f; // Retraso entre cada disparo de la ráfaga

    public void Ability()
    {
        StartCoroutine(DelayShoot());
    }

    // Corrutina que controla el retraso entre cada disparo de la ráfaga
    IEnumerator DelayShoot()
    {
        for (int i = 0; i < shoots; i++)
        {
            // Cambia al siguiente proyectil en el pool
            AddPool();

            // Selecciona la bala que se va a disparar
            Bullet();

            // Asigna los atributos del proyectil (daño, rango, etc.)
            SetAtributes();

            // Aplica la fuerza para disparar el proyectil
            AddForce(0);

            // Espera un tiempo antes de disparar el siguiente proyectil
            yield return new WaitForSeconds(delayShoot);
        }

        StartCD();
    }
}
