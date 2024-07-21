using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    private IStats statsType;
    [HideInInspector]
    public IBulletType effectType;

    [HideInInspector]
    public float speed, dmg, timeToDie;
    [HideInInspector]
    public Vector3 target;
    [HideInInspector]
    public LayerMask hitLayer;
    [HideInInspector]
    public bool lifeTime;

    private void OnEnable()
    {
        GetComponent<Rigidbody2D>().AddForce(Direction() * speed, ForceMode2D.Force);

        if (lifeTime)
        {
            StartCoroutine(LifeTime());
        }
        
    }
    private void OnBecameInvisible()
    {
        EndProjectile();
    }

    private Vector2 Direction()
    {
        Vector2 direction = (target - transform.position).normalized;

        return direction;
    }

    IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(timeToDie);

        EndProjectile();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & hitLayer) != 0)
        {
            statsType = other.GetComponent<IStats>();

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
