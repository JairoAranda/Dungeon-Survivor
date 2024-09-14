using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour, IEffectType
{
    [Range(0.1f, 5f)]
    [SerializeField] float explosiveDmg = 0.5f;

    [SerializeField] LayerMask hitMask;

    public void Effect(GameObject target)
    {
        ExplosionParticlePool.instance.SpawnExplosion(target, explosiveDmg, hitMask);
    }
}
