using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionTrigger : MonoBehaviour
{
    [HideInInspector]
    public LayerMask hitLayer; // Capa(s) en la que el explosión puede causar daño

    [HideInInspector]
    public float dmg; // Daño causado por la explosión

    private IStats statsType; // Interfaz para obtener información de salud de los objetos

    ParticleSystem particles; // Sistema de partículas para la explosión

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica si el objeto que colisiona está en la capa especificada
        if (((1 << other.gameObject.layer) & hitLayer) != 0)
        {
            // Obtiene el componente IStats del objeto colisionado
            statsType = other.GetComponent<IStats>();

            // Si el objeto tiene el componente IStats, aplica el daño
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

        // Desactiva el collider del círculo
        GetComponent<CircleCollider2D>().enabled = false;
    }
}
