using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public class GigantShoot : BaseRangedAbility, IAbility
{
    [Header("Scale Conifg")]
    [Range(1.1f , 8f)]
    [SerializeField] float scaleMultiplier = 5; // Multiplicador de tama�o que se alcanzar� cuando el proyectil alcance distancia maxima 

    [Range(1.1f, 8f)]
    [SerializeField] float targetDmgMultiplier = 2f;   // Multiplicador de da�o que se alcanzar� cuando el proyectil alcance su tama�o m�ximo

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

        // Asigna los atributos del proyectil (da�o, rango, etc.)
        SetAtributes();

        // Inicia la corrutina que aumentar� el tama�o y el da�o del proyectil a lo largo del tiempo
        StartCoroutine(ScaleOverTime());

        // Aplica la fuerza para disparar el proyectil
        AddForce(0);
    }


    IEnumerator ScaleOverTime()
    {
        // Calcula la duraci�n del trayecto en funci�n del rango y la velocidad del proyectil
        float duration = sOPlayerInfo.range / sOPlayerInfo.projectSpeed;

        float elapsedTime = 0f;

        // Almacena el tama�o inicial del proyectil
        Vector3 initialScale = bulletToShoot.transform.localScale;

        // Almacena el tama�o final del proyectil
        Vector3 targetScale = initialScale * scaleMultiplier;

        // Obtiene el componente Projectile del proyectil
        Projectile projectile = bulletToShoot.GetComponent<Projectile>();

        // Almacena el da�o inicial del proyectil
        float initialDmg = projectile.dmg;

        // Calcula el da�o objetivo multiplicando el da�o inicial por el multiplicador
        float targetDmg = initialDmg * targetDmgMultiplier;

        // Aumenta el tama�o y el da�o del proyectil de forma progresiva hasta que se completa la duraci�n
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            // Cambia el tama�o del proyectil usando interpolaci�n lineal
            bulletToShoot.transform.localScale = Vector3.Lerp(initialScale, targetScale, elapsedTime / duration);

            // Cambia el da�o del proyectil usando interpolaci�n lineal
            projectile.dmg = Mathf.Lerp(initialDmg, targetDmg, elapsedTime / duration);

            yield return null;
        }

        // Restablece el tama�o del proyectil al valor inicial
        bulletToShoot.transform.localScale = initialScale;
    }

}
