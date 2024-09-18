using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionTrigger : MonoBehaviour
{
    [HideInInspector]
    public LayerMask hitLayer; // Capa(s) en la que el explosi�n puede causar da�o

    [HideInInspector]
    public float dmg; // Da�o causado por la explosi�n

    private IStats statsType; // Interfaz para obtener informaci�n de salud de los objetos

    ParticleSystem particles; // Sistema de part�culas para la explosi�n

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica si el objeto que colisiona est� en la capa especificada
        if (((1 << other.gameObject.layer) & hitLayer) != 0)
        {
            // Obtiene el componente IStats del objeto colisionado
            statsType = other.GetComponent<IStats>();

            // Si el objeto tiene el componente IStats, aplica el da�o
            if (statsType != null)
            {
                statsType.GetHit(dmg);
            }
        }
    }

    private void OnEnable()
    {
        particles = GetComponent<ParticleSystem>();

        StartCoroutine(DisableCollider());
    }

    IEnumerator DisableCollider()
    {
        yield return new WaitForSeconds(particles.main.duration);

        // Desactiva este script
        this.enabled = false;

        // Desactiva el collider del c�rculo
        GetComponent<CircleCollider2D>().enabled = false;
    }
}
