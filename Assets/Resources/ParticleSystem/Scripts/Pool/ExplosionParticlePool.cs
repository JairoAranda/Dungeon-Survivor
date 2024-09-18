using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionParticlePool : GeneralPool
{
    public static ExplosionParticlePool instance;

    int explosionNumber = -1; // �ndice para rastrear el siguiente objeto de part�culas de explosi�n a usar

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

        // Si el �ndice excede el tama�o del pool, rein�cialo a 0 para reutilizar objetos
        if (explosionNumber > poolSize - 1)
        {
            explosionNumber = 0;
        }

        // Obtiene el siguiente objeto de part�culas de explosi�n del pool
        GameObject explosion = typesInstances[explosionNumber];

        // Establece la posici�n del objeto de part�culas de explosi�n en la posici�n del objeto afectado
        explosion.transform.position = go.transform.position;

        // Reproduce el sistema de part�culas
        explosion.GetComponent<ParticleSystem>().Play();

        // Configura el componente ExplosionTrigger del objeto de part�culas de explosi�n
        ExplosionTrigger explosionTrigger = explosion.GetComponent<ExplosionTrigger>();
        explosionTrigger.hitLayer = hitMask; // Establece la m�scara de capa para los objetos que ser�n afectados
        explosionTrigger.dmg = _dmg; // Establece el da�o de la explosi�n
        explosionTrigger.enabled = true; // Habilita el componente para que pueda detectar colisiones

        // Habilita el componente Collider2D del objeto de part�culas de explosi�n
        explosion.GetComponent<CircleCollider2D>().enabled = true;

    }
}
