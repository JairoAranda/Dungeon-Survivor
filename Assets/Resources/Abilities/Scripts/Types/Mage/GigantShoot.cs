using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public class GigantShoot : BaseRangedAbility, IAbility
{
    [Header("Scale Conifg")]
    [Range(1.1f , 8f)]
    [SerializeField] float scaleMultiplier = 5; // Multiplicador de tamaño que se alcanzará cuando el proyectil alcance distancia maxima 

    [Range(1.1f, 8f)]
    [SerializeField] float targetDmgMultiplier = 2f;   // Multiplicador de daño que se alcanzará cuando el proyectil alcance su tamaño máximo

    protected override void OnDisable()
    {
        base.OnDisable();

        StopAllCoroutines();
    }

    public void Ability()
    {
        // Cambia al siguiente proyectil en el pool
        AddPool();

        // Selecciona la bala que se va a disparar
        Bullet();

        // Asigna los atributos del proyectil (daño, rango, etc.)
        SetAtributes();

        // Inicia la corrutina que aumentará el tamaño y el daño del proyectil a lo largo del tiempo
        StartCoroutine(ScaleOverTime());

        // Aplica la fuerza para disparar el proyectil
        AddForce(0);
    }


    IEnumerator ScaleOverTime()
    {
        // Calcula la duración del trayecto en función del rango y la velocidad del proyectil
        float duration = sOPlayerInfo.range / sOPlayerInfo.projectSpeed;

        float elapsedTime = 0f;

        // Almacena el tamaño inicial del proyectil
        Vector3 initialScale = bulletToShoot.transform.localScale;

        // Almacena el tamaño final del proyectil
        Vector3 targetScale = initialScale * scaleMultiplier;

        // Obtiene el componente Projectile del proyectil
        Projectile projectile = bulletToShoot.GetComponent<Projectile>();

        // Almacena el daño inicial del proyectil
        float initialDmg = projectile.dmg;

        // Calcula el daño objetivo multiplicando el daño inicial por el multiplicador
        float targetDmg = initialDmg * targetDmgMultiplier;

        // Aumenta el tamaño y el daño del proyectil de forma progresiva hasta que se completa la duración
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            // Cambia el tamaño del proyectil usando interpolación lineal
            bulletToShoot.transform.localScale = Vector3.Lerp(initialScale, targetScale, elapsedTime / duration);

            // Cambia el daño del proyectil usando interpolación lineal
            projectile.dmg = Mathf.Lerp(initialDmg, targetDmg, elapsedTime / duration);

            yield return null;
        }

        // Restablece el tamaño del proyectil al valor inicial
        bulletToShoot.transform.localScale = initialScale;
    }

}
