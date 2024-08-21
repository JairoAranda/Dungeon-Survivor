using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    private IStats statsType;

    [HideInInspector]
    public GameObject owner;

    [HideInInspector]
    public float dmg, range;
    [HideInInspector]
    public LayerMask hitLayer;

    IBulletType effectType;

    private Vector2 startPosition;

    private void OnEnable()
    {
        startPosition = transform.position;
        
    }
    private void OnBecameInvisible()
    {
        EndProjectile();

    }

    private void Update()
    {
        float distanceTravelled = Vector2.Distance(startPosition, transform.position);

        if (distanceTravelled >= range )
        {
            effectType = owner.GetComponentInChildren<IBulletType>();

            if (effectType != null )
            {
                effectType.Effect(gameObject);
            }

            EndProjectile();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & hitLayer) != 0)
        {
            statsType = other.GetComponent<IStats>();

            effectType = owner.GetComponentInChildren<IBulletType>();

            if (statsType != null && effectType != null)
            {
                statsType.GetHit(dmg);

                effectType.Effect(other.gameObject);
            }

            EndProjectile();
        }
    }

    void EndProjectile()
    {
        gameObject.transform.position = new Vector3(0, 0);
        gameObject.SetActive(false);
    }


}
