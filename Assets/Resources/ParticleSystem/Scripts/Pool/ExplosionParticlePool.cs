using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionParticlePool : GeneralPool
{
    public static ExplosionParticlePool instance;

    int explosionNumber = -1;

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

        if (explosionNumber > poolSize - 1)
        {
            explosionNumber = 0;
        }

        GameObject explosion = typesInstances[explosionNumber];

        explosion.transform.position = go.transform.position;

        explosion.GetComponent<ParticleSystem>().Play();

        ExplosionTrigger explosionTrigger = explosion.GetComponent<ExplosionTrigger>();

        explosionTrigger.hitLayer = hitMask;

        explosionTrigger.dmg = _dmg;

        explosionTrigger.enabled = true;

        explosion.GetComponent<CircleCollider2D>().enabled = true;

    }
}
