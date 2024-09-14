using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionTrigger : MonoBehaviour
{
    [HideInInspector]
    public LayerMask hitLayer;

    [HideInInspector]
    public float dmg;

    private IStats statsType;

    ParticleSystem particles;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & hitLayer) != 0)
        {
            statsType = other.GetComponent<IStats>();

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

        this.enabled = false;

        GetComponent<CircleCollider2D>().enabled = false;
    }
}
