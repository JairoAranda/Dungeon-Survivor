using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour, IEffectType
{
    [Range(0.1f, 5f)]
    [SerializeField] float explosiveDmg = 0.5f; // Da�o de la explosi�n. Controla la intensidad de la explosi�n.

    [SerializeField] LayerMask hitMask; // M�scara de capas para determinar qu� objetos ser�n afectados por la explosi�n.

    // Aplica el efecto de explosi�n al objetivo especificado.
    public void Effect(GameObject target)
    {
        // Llama al m�todo para generar la explosi�n y aplicar da�o
        ExplosionParticlePool.instance.SpawnExplosion(target, explosiveDmg, hitMask);
    }
}
