using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TripleShoot : BaseRangedAbility, IAbility
{
    [Header("Angle Config")]
    [Space]
    [Range(.1f, 40f)]
    [SerializeField] float shotAngle; // �ngulo de apertura para el disparo en abanico


    public void Ability()
    {
        // Dispara proyectiles en un �ngulo determinado
        for (float i = -shotAngle; i <= shotAngle; i+=shotAngle) 
        {
            // Cambia al siguiente proyectil en el pool
            AddPool();

            // Selecciona la bala que se va a disparar
            Bullet();

            // Asigna los atributos del proyectil (da�o, rango, etc.)
            SetAtributes();

            // Aplica la fuerza al proyectil en el �ngulo actual
            AddForce(i);
        }
        
    }

}
