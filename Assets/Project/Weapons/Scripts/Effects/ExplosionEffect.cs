using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour, IEffectType
{
    [Range(0.1f, 5f)]
    [SerializeField] float explosiveDmg = 0.5f; // Daño de la explosión. Controla la intensidad de la explosión.

    [SerializeField] LayerMask hitMask; // Máscara de capas para determinar qué objetos serán afectados por la explosión.

    // Aplica el efecto de explosión al objetivo especificado.
    public void Effect(GameObject target)
    {
        // Llama al método para generar la explosión y aplicar daño
        ExplosionParticlePool.instance.SpawnExplosion(target, explosiveDmg, hitMask);
    }
}
