using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionParticlePool : GeneralPool
{
    public static ExplosionParticlePool instance;

    int explosionNumber = -1; // Índice para rastrear el siguiente objeto de partículas de explosión a usar

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void SpawnExplosion(GameObject go, float _dmg, LayerMask hitMask)
    {
        explosionNumber++;

        // Si el índice excede el tamaño del pool, reinícialo a 0 para reutilizar objetos
        if (explosionNumber > poolSize - 1)
        {
            explosionNumber = 0;
        }

        // Obtiene el siguiente objeto de partículas de explosión del pool
        GameObject explosion = typesInstances[explosionNumber];

        // Establece la posición del objeto de partículas de explosión en la posición del objeto afectado
        explosion.transform.position = go.transform.position;

        // Reproduce el sistema de partículas
        explosion.GetComponent<ParticleSystem>().Play();

        // Configura el componente ExplosionTrigger del objeto de partículas de explosión
        ExplosionTrigger explosionTrigger = explosion.GetComponent<ExplosionTrigger>();
        explosionTrigger.hitLayer = hitMask; // Establece la máscara de capa para los objetos que serán afectados
        explosionTrigger.dmg = _dmg; // Establece el daño de la explosión
        explosionTrigger.enabled = true; // Habilita el componente para que pueda detectar colisiones

        // Habilita el componente Collider2D del objeto de partículas de explosión
        explosion.GetComponent<CircleCollider2D>().enabled = true;

    }
}
